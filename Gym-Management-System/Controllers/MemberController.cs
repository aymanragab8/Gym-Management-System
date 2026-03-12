using GymSystem.Application.Dtos.MemberDto;
using GymSystem.Application.Interfaces;
using GymSystem.Domain.AppMeteData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Gym_Management_System.Controllers
{
    [Authorize]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }
        [HttpGet(Router.Member.GetAllMembers)]
        [Authorize(Roles = "Admin,Coach")]
        public async Task<IActionResult> GetAllMembers(int pageNumber = 1, int pageSize = 10)
        {
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var members = await _memberService.GetAllMembersAsync(pageNumber, pageSize, userId, role);
            return Ok(members);
        }
        [HttpGet(Router.Member.GetById)]
        public async Task<IActionResult> GetSingleMember(int id)
        {
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var member = await _memberService.GetMemberByIdAsync(id, userId, role);
            return Ok(member);
        }
        [HttpPost(Router.Member.CreateMember)]
        public async Task<IActionResult> CreateMember(CreateMemberDto dto)
        {
            var member = await _memberService.CreateMemberAsync(dto);
            return Ok(member);
        }
        [HttpPut(Router.Member.UpdateMember)]
        public async Task<IActionResult> UpdateMemberData(int id, UpdateMemberDto dto)
        {
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var member = await _memberService.UpdateMemberAsync(id, dto, userId, role);
            return Ok(member);
        }
        [HttpDelete(Router.Member.DeleteMember)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteMember(int id)
        {
            var member = await _memberService.DeleteMemberAsync(id);
            return Ok(member);
        }
        [HttpGet(Router.Member.ViewSubscriptions)]
        [Authorize(Roles = "Admin,Member")]
        public async Task<IActionResult> GetMemberSubscriptions(int memberId)
        {
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var member = await _memberService.GetMemberSubscriptionsAsync(memberId, userId, role);
            return Ok(member);
        }
    }
}
