namespace GymSystem.Domain.Entities
{
    public class Attendance : BaseEntity
    {
        public int MemberId { get; set; }
        public Member? Member { get; set; }
    }
}
