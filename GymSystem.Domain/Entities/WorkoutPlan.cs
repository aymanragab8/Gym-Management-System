
namespace GymSystem.Domain.Entities
{
    public class WorkoutPlan : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        
        public int MemberId { get; set; }
        public Member? Member { get; set; }
        public ICollection<WorkoutPlanExercise>? WorkoutPlanExercises { get; set; }
    }
}
