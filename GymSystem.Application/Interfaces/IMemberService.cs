using GymSystem.Application.Dtos.MemberDto;

namespace GymSystem.Application.Interfaces
{
    public interface IMemberService
    {
        public Task<List<MembersDataDto>> GetAllMembersAsync(int pageNumber, int pageSize, string currentUserId, string currentUserRole);
        public Task<MembersDataDto> GetMemberByIdAsync(int id, string currentUserId, string currentUserRole);
        public Task<MemberSubscriptionsDto> GetMemberSubscriptionsAsync(int memberId, string currentUserId, string currentUserRole);
        public Task<string> CreateMemberAsync(CreateMemberDto dto);
        public Task<string> UpdateMemberAsync(int memberId, UpdateMemberDto dto, string currentUserId, string currentUserRole);
        public Task<string> DeleteMemberAsync(int memberId);
    }
}
