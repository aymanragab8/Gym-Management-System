
namespace GymSystem.Domain.Entities
{
    public class Exercise : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<WorkoutPlanExercise>? WorkoutPlanExercises { get; set; }
    }
}
