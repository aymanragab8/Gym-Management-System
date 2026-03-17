using GymSystem.Application.Dtos.Auth;

namespace GymSystem.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegisterDto dto);
        Task<AuthResponseDto> LoginAsync(LoginDto dto);
        Task<AuthResponseDto> RefreshTokenAsync(string refreshToken);
        Task<string> RevokeTokenAsync(string refreshToken);

    }
}
