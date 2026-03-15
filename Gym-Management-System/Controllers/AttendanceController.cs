using GymSystem.Application.Interfaces;
using GymSystem.Domain.AppMeteData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Gym_Management_System.Controllers
{
    [ApiController]
    [Authorize]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;

        public AttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        [HttpPost(Router.Attendance.CheckIn)]
        [Authorize(Roles = "Member")]
        public async Task<IActionResult> CheckIn()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _attendanceService.CheckIn(currentUserId);
            return Ok(result);
        }
        [HttpGet(Router.Attendance.GetMemberAttendance)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetMemberAttendance(int memberId)
        {
            var attendanceData = await _attendanceService.MemberAttendance(memberId);
            return Ok(attendanceData);
        }
        [HttpGet(Router.Attendance.GetMemberAttendanceByDate)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetMembersAttendanceByDate(DateTime date)
        {
            var attendanceData = await _attendanceService.MemberAttendanceByDate(date);
            return Ok(attendanceData);
        }
        [HttpDelete(Router.Attendance.DeleteMemberAttendance)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteMemberAttendance(int memberId)
        {
            var result = await _attendanceService.DeleteMmberAttendance(memberId);
            return Ok(result);
        }
    }
}
