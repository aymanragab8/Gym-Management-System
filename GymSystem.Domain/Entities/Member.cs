using GymSystem.Domain.Enums;

namespace GymSystem.Domain.Entities
{
    public class Member : BaseEntity
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public MemberStatus Status { get; set; } = MemberStatus.Active;
        public MemberType MemberType { get; set; } = MemberType.Member;

        public string? ApplicationUserId { get; set; }

        public ICollection<Subscription>? Subscriptions { get; set; }
        public ICollection<WorkoutPlan>? WorkoutPlans { get; set; }
        public ICollection<Attendance>? Attendances { get; set; }
        public ICollection<Payment>? Payments { get; set; }
        public int? CoachId { get; set; }
        public virtual Member? Coach { get; set; }
        public virtual ICollection<Member>? CoachedMembers { get; set; } = new List<Member>();


        public string? Specialization { get; set; }
        public decimal? HourlyRate { get; set; }
    }
}