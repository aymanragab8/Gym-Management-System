namespace GymSystem.Application.Dtos.WorkoutPlan
{
    public class WorkoutPlanExerciseDataDto
    {
        public int ExerciseId { get; set; }
        public string ExerciseName { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
