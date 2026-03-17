namespace GymSystem.Application.Dtos.WorkoutPlan
{
    public class WorkoutPlanDataDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string MemberName { get; set; }
    }
}
