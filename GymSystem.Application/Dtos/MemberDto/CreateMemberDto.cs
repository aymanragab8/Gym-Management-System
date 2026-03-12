namespace GymSystem.Application.Dtos.MemberDto
{
    public class CreateMemberDto
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
    }
}
