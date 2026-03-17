using AutoMapper;
using GymSystem.Application.Dtos.Auth;
using GymSystem.Application.Interfaces;
using GymSystem.Domain.Entities;
using GymSystem.Infrastructure.Identity;
using GymSystem.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebApplication2.Models.Data;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IConfiguration _configuration;
    private readonly IMemberRepository _memberRepository;
    private readonly IMapper _mapper;
    private readonly ApplicationDbContext _context;

    public AuthService(UserManager<ApplicationUser> userManager,
                       SignInManager<ApplicationUser> signInManager,
                       IConfiguration configuration,
                       IMemberRepository memberRepository,
                       IMapper mapper,
                       ApplicationDbContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
        _memberRepository = memberRepository;
        _mapper = mapper;
        _context = context;
    }

    public async Task<string> RegisterAsync(RegisterDto dto)
    {
        var existingUser = await _userManager.FindByEmailAsync(dto.Email);
        if (existingUser != null)
            throw new ArgumentException("Email already exists.");

        var user = new ApplicationUser
        {
            UserName = dto.Email,
            Email = dto.Email,
        };

        var result = await _userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded)
            throw new ArgumentException(string.Join(", ", result.Errors.Select(e => e.Description)));

        await _userManager.AddToRoleAsync(user, "Member");

        var member = _mapper.Map<Member>(dto);
        member.ApplicationUserId = user.Id;
        await _memberRepository.AddAsync(member);

        return "Registration successful.";
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user == null)
            throw new KeyNotFoundException("User not found.");

        var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
        if (!result.Succeeded)
            throw new ArgumentException("Invalid password.");

        var roles = await _userManager.GetRolesAsync(user);
        var accessToken = await GenerateJwtToken(user);
        var refreshToken = await GenerateRefreshToken(user.Id);

        var role = roles.FirstOrDefault() ?? "";
        return BuildAuthResponse(user, role, accessToken, refreshToken);
    }

    public async Task<AuthResponseDto> RefreshTokenAsync(string refreshToken)
    {
        var storedToken = await _context.RefreshTokens
            .FirstOrDefaultAsync(x => x.Token == refreshToken);

        if (storedToken == null)
            throw new UnauthorizedAccessException("Invalid refresh token.");

        if (storedToken.IsRevoked)
            throw new UnauthorizedAccessException("Refresh token has been revoked.");

        if (storedToken.ExpiresAt < DateTime.UtcNow)
            throw new UnauthorizedAccessException("Refresh token has expired.");

        var user = await _userManager.FindByIdAsync(storedToken.UserId);
        if (user == null)
            throw new KeyNotFoundException("User not found.");

        storedToken.IsRevoked = true;
        await _context.SaveChangesAsync();

        var roles = await _userManager.GetRolesAsync(user);
        var newAccessToken = await GenerateJwtToken(user);
        var newRefreshToken = await GenerateRefreshToken(user.Id);

        var role = roles.FirstOrDefault() ?? "";
        return BuildAuthResponse(user, role, newAccessToken, newRefreshToken);
    }

    public async Task<string> RevokeTokenAsync(string refreshToken)
    {
        var storedToken = await _context.RefreshTokens
            .FirstOrDefaultAsync(x => x.Token == refreshToken);

        if (storedToken == null)
            throw new KeyNotFoundException("Refresh token not found.");

        if (storedToken.IsRevoked)
            throw new InvalidOperationException("Token already revoked.");

        storedToken.IsRevoked = true;
        await _context.SaveChangesAsync();

        return "Token revoked successfully.";
    }

    private async Task<string> GenerateRefreshToken(string userId)
    {
        var refreshToken = new RefreshToken
        {
            Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            UserId = userId,
            IsRevoked = false
        };

        await _context.RefreshTokens.AddAsync(refreshToken);
        await _context.SaveChangesAsync();

        return refreshToken.Token;
    }

    private async Task<string> GenerateJwtToken(ApplicationUser user)
    {
        var roles = await _userManager.GetRolesAsync(user);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.UserName)
        };

        foreach (var role in roles)
            claims.Add(new Claim(ClaimTypes.Role, role));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:IssuerUrl"],
            audience: _configuration["Jwt:AudienceUrl"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(15),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    private AuthResponseDto BuildAuthResponse(ApplicationUser user, string role, string accessToken, string refreshToken)
    {
        return new AuthResponseDto
        {
            Email = user.Email,
            Role = role,
            Token = accessToken,
            TokenExpiry = DateTime.UtcNow.AddMinutes(15),
            RefreshToken = refreshToken,
            RefreshTokenExpiry = DateTime.UtcNow.AddDays(7)
        };
    }
}