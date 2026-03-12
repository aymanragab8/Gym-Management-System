using GymSystem.Domain.Enums;

namespace GymSystem.Application.Dtos.MemberDto
{
    public class UpdateMemberDto
    {
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Specialization { get; set; }
        public decimal? HourlyRate { get; set; }
        public int? CoachId { get; set; }
        public MemberStatus? Status { get; set; }
        public MemberType? MemberType { get; set; }


    }
}
