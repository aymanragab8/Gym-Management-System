using GymSystem.Application.Dtos.WorkoutPlan;
using GymSystem.Application.Interfaces;
using GymSystem.Domain.AppMeteData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Gym_Management_System.Controllers
{
    [ApiController]

    public class WorkoutPlanController : ControllerBase
    {
        private readonly IWorkoutPlanService _workoutPlanService;

        public WorkoutPlanController(IWorkoutPlanService workoutPlanService)
        {
            _workoutPlanService = workoutPlanService;
        }
        [HttpGet(Router.WorkoutPlan.GetAllWorkoutPlans)]
        [Authorize(Roles = "Coach")]
        public async Task<IActionResult> GetAllWorkoutPlans(int pageNumber = 1, int pageSize = 10)
        {
            var workoutPlans = await _workoutPlanService.GetAllAsync(pageNumber, pageSize);
            return Ok(workoutPlans);
        }
        [HttpGet(Router.WorkoutPlan.GetAllWorkoutPlansforMemeber)]
        [Authorize(Roles = "Coach,Member")]
        public async Task<IActionResult> GetAllWorkoutPlansForMember(int memberId)
        {
            string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string role = User.FindFirstValue(ClaimTypes.Role);
            var workoutPlans = await _workoutPlanService.GetAllWorkoutPlanforMemberAsync(memberId, currentUserId, role);
            return Ok(workoutPlans);
        }
        [HttpPost(Router.WorkoutPlan.CreateWorkoutPlan)]
        [Authorize(Roles = "Coach")]
        public async Task<IActionResult> AddWorkoutPlanForMember(int memberId, CreateWorkoutPlanDto dto)
        {
            var workoutPlans = await _workoutPlanService.CreateWorkoutPlanAsync(memberId, dto);
            return Ok(workoutPlans);
        }
        [HttpPut(Router.WorkoutPlan.UpdateWorkoutPlan)]
        [Authorize(Roles = "Coach")]
        public async Task<IActionResult> UpdateWorkoutPlanForMember(int workplanId, UpdateWorkoutPlanDto dto)
        {
            var workoutPlan = await _workoutPlanService.UpdateWorkoutPlanAsync(workplanId, dto);
            return Ok(workoutPlan);
        }
        [HttpDelete(Router.WorkoutPlan.DeleteWorkoutPlan)]
        [Authorize(Roles = "Coach")]
        public async Task<IActionResult> DeleteWorkoutPlanForMember(int workplanId)
        {
            var workoutPlan = await _workoutPlanService.DeleteWorkoutPlanAsync(workplanId);
            return Ok(workoutPlan);
        }
        [HttpPost(Router.WorkoutPlan.CreateExerciseToWorkoutPlan)]
        [Authorize(Roles = "Coach")]
        public async Task<IActionResult> AddExerciseToWorkoutPlan(int workplanId, int exId, CreateExerciseToWorkoutPlanDto dto)
        {
            var result = await _workoutPlanService.AddExerciseToWorkoutPlanAsync(workplanId, exId, dto);
            return Ok(result);
        }
        [HttpDelete(Router.WorkoutPlan.DeleteExerciseFromWorkoutPlan)]
        [Authorize(Roles = "Coach")]
        public async Task<IActionResult> RemoveExerciseFromWorkoutPlan(int workplanId, int exId)
        {
            var result = await _workoutPlanService.RemoveExerciseFromWorkoutPlanAsync(workplanId, exId);
            return Ok(result);
        }
        [HttpGet(Router.WorkoutPlan.GetExercisesByWorkoutPlanId)]
        [Authorize(Roles = "Coach,Member")]
        public async Task<IActionResult> GetExercisesByWorkoutPlanId(int workplanId)
        {
            string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string role = User.FindFirstValue(ClaimTypes.Role);
            var result = await _workoutPlanService.GetExercisesByWorkoutPlanIdAsync(workplanId, currentUserId, role);
            return Ok(result);
        }
        [HttpPut(Router.WorkoutPlan.UpdateWorkoutPlanExercise)]
        [Authorize(Roles = "Coach")]
        public async Task<IActionResult> UpdateWorkoutPlanExercise(int workplanId, int exId, UpdateWorkoutPlanExerciseDto dto)
        {
            var result = await _workoutPlanService.UpdateWorkoutPlanExerciseAsync(workplanId, exId, dto);
            return Ok(result);
        }
    }
}
