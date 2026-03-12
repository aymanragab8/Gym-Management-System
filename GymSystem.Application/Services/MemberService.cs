using AutoMapper;
using GymSystem.Application.Abstracts;
using GymSystem.Application.Dtos.MemberDto;
using GymSystem.Application.Interfaces;
using GymSystem.Domain.Entities;
using GymSystem.Domain.Enums;
using GymSystem.Infrastructure.Interfaces;

namespace GymSystem.Application.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IMapper _mapper;

        public MemberService(IMemberRepository memberRepository, ISubscriptionRepository subscriptionRepository, IMapper mapper)
        {
            _memberRepository = memberRepository;
            _subscriptionRepository = subscriptionRepository;
            _mapper = mapper;
        }

        public async Task<string> CreateMemberAsync(CreateMemberDto dto)
        {
            var member = _mapper.Map<Member>(dto);

            await _memberRepository.AddAsync(member);

            return $"Member {member.FullName} created successfully with Id: {member.Id}.";
        }

        public async Task<string> DeleteMemberAsync(int memberId)
        {
            if (memberId <= 0)
                throw new ArgumentException("MemberId must be greater than 0.");

            var member = await _memberRepository.GetByIdAsync(memberId);

            if (member == null)
                throw new KeyNotFoundException($"Member with id {memberId} not found.");

            await _memberRepository.DeleteAsync(member);

            return $"Member {member.FullName} deleted successfully.";
        }

        public async Task<List<MembersDataDto>> GetAllMembersAsync(int pageNumber, int pageSize, string currentUserId, string currentUserRole)
        {
            var members = await _memberRepository.GetAllAsync(pageNumber, pageSize);
            switch (currentUserRole)
            {
                case "Coach":
                    var currentCoach = await _memberRepository.GetByApplicationUserIdAsync(currentUserId);

                    if (currentCoach == null)
                        throw new UnauthorizedAccessException("Coach not found or invalid for the current user.");

                    if (currentCoach.MemberType != MemberType.Coach)
                        throw new UnauthorizedAccessException("User is not a Coach.");

                    var coachId = currentCoach.Id;

                    members = members.Where(m => m.CoachId == coachId).ToList();
                    break;

                case "Admin":
                    break;

                default:
                    throw new UnauthorizedAccessException("Unknown role.");
            }

            if (!members.Any())
                return new List<MembersDataDto>();

            return _mapper.Map<List<MembersDataDto>>(members);
        }

        public async Task<MembersDataDto> GetMemberByIdAsync(int id, string currentUserId, string currentUserRole)
        {
            if (id <= 0)
                throw new ArgumentException("Id must be a positive integer.", nameof(id));

            var member = await _memberRepository.GetByIdAsync(id);

            if (member == null)
                throw new KeyNotFoundException($"Member with id {id} not found.");

            switch (currentUserRole)
            {
                case "Member":
                    if (member.ApplicationUserId != currentUserId)
                        throw new UnauthorizedAccessException("You can only view your own data.");
                    break;

                case "Coach":
                    var currentCoach = await _memberRepository.GetByApplicationUserIdAsync(currentUserId);

                    if (currentCoach == null)
                        throw new UnauthorizedAccessException("Coach not found or invalid for the current user.");

                    if (currentCoach.MemberType != MemberType.Coach)
                        throw new UnauthorizedAccessException("User is not a Coach.");

                    var coachId = currentCoach.Id;

                    if (member.CoachId != coachId)
                        throw new UnauthorizedAccessException("You can only view members assigned to you.");
                    break;

                case "Admin":
                    break;

                default:
                    throw new UnauthorizedAccessException("Unknown role.");
            }

            return _mapper.Map<MembersDataDto>(member);
        }

        public async Task<MemberSubscriptionsDto> GetMemberSubscriptionsAsync(int memberId, string currentUserId, string currentUserRole)
        {
            if (memberId <= 0)
                throw new ArgumentException("Id must be a positive integer.", nameof(memberId));
            var member = await _memberRepository.GetByIdAsync(memberId);

            if (member == null)
                throw new KeyNotFoundException($"Member with id {memberId} not found.");
            var subs = await _subscriptionRepository.GetAllAsync();

            switch (currentUserRole)
            {
                case "Member":
                    if (member.ApplicationUserId != currentUserId)
                        throw new UnauthorizedAccessException("You can only view your own Subscriptions.");
                    break;

                case "Admin":
                    break;

                default:
                    throw new UnauthorizedAccessException("Unknown role.");
            }
            return _mapper.Map<MemberSubscriptionsDto>(member);
        }

        public async Task<string> UpdateMemberAsync(int memberId, UpdateMemberDto dto, string currentUserId, string currentUserRole)
        {
            if (memberId <= 0)
                throw new ArgumentException("MemberId must be greater than 0.");

            var member = await _memberRepository.GetByIdAsync(memberId);

            if (member == null)
                throw new KeyNotFoundException($"Member with id {memberId} not found.");

            switch (currentUserRole)
            {
                case "Member":

                    if (member.ApplicationUserId != currentUserId)
                        throw new UnauthorizedAccessException("You can only update your own data.");

                    if (!string.IsNullOrEmpty(dto.Email))
                        member.Email = dto.Email;

                    if (!string.IsNullOrEmpty(dto.PhoneNumber))
                        member.PhoneNumber = dto.PhoneNumber;

                    break;

                case "Coach":

                    if (member.ApplicationUserId != currentUserId)
                        throw new UnauthorizedAccessException("You can only update your own data.");

                    if (!string.IsNullOrEmpty(dto.Email))
                        member.Email = dto.Email;

                    if (!string.IsNullOrEmpty(dto.PhoneNumber))
                        member.PhoneNumber = dto.PhoneNumber;

                    if (!string.IsNullOrEmpty(dto.Specialization))
                        member.Specialization = dto.Specialization;

                    break;

                case "Admin":

                    if (!string.IsNullOrEmpty(dto.Email))
                        member.Email = dto.Email;

                    if (!string.IsNullOrEmpty(dto.PhoneNumber))
                        member.PhoneNumber = dto.PhoneNumber;

                    if (!string.IsNullOrEmpty(dto.Specialization))
                        member.Specialization = dto.Specialization;

                    if (dto.HourlyRate.HasValue)
                        member.HourlyRate = dto.HourlyRate;

                    if (dto.Status.HasValue)
                        member.Status = dto.Status.Value;

                    if (dto.MemberType.HasValue)
                        member.MemberType = dto.MemberType.Value;

                    break;

                default:
                    throw new UnauthorizedAccessException("Unknown role.");
            }

            await _memberRepository.UpdateAsync(member);

            return $"User {member.FullName} updated successfully.";
        }
    }
}