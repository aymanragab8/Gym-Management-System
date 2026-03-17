using GymSystem.Application.Dtos.ExerciseDto;
using GymSystem.Application.Interfaces;
using GymSystem.Domain.AppMeteData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gym_Management_System.Controllers
{
    [ApiController]
    [Authorize]
    public class ExerciseController : ControllerBase
    {
        private readonly IExerciseService _exerciseService;

        public ExerciseController(IExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
        }
        [HttpGet(Router.Exercise.GetAllExercises)]
        public async Task<IActionResult> GetAllExercises(int pageNumber = 1, int pageSize = 10)
        {
            var result = await _exerciseService.GetAllExercisesAsync(pageNumber, pageSize);
            return Ok(result);
        }
        [HttpPost(Router.Exercise.CreateExercise)]
        [Authorize(Roles = "Coach")]
        public async Task<IActionResult> AddExercise(CreateExerciseDto dto)
        {
            var result = await _exerciseService.AddExerciseAsync(dto);
            return Ok(result);
        }
        [HttpDelete(Router.Exercise.DeleteExercise)]
        [Authorize(Roles = "Coach")]
        public async Task<IActionResult> DeleteExercise(int exId)
        {
            var result = await _exerciseService.DeleteExerciseAsync(exId);
            return Ok(result);
        }
    }
}
