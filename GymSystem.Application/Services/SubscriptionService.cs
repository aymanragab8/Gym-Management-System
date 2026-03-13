using AutoMapper;
using GymSystem.Application.Abstracts;
using GymSystem.Application.Dtos.SubscriptionDto;
using GymSystem.Application.Interfaces;
using GymSystem.Domain.Entities;
using GymSystem.Domain.Enums;
using GymSystem.Infrastructure.Interfaces;

namespace GymSystem.Application.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;

        public SubscriptionService(
            ISubscriptionRepository subscriptionRepository,
            IMemberRepository memberRepository,
            IMapper mapper)
        {
            _subscriptionRepository = subscriptionRepository;
            _memberRepository = memberRepository;
            _mapper = mapper;
        }

        public async Task<string> AddSubscriptionAsync(int memberId, CreateSubscriptionDto dto)
        {
            if (memberId <= 0)
                throw new ArgumentException("MemberId must be greater than 0.");

            var member = await _memberRepository.GetByIdAsync(memberId);
            if (member == null)
                throw new KeyNotFoundException($"Member with id {memberId} not found.");

            var hasActive = await _subscriptionRepository.HasActiveSubscriptionAsync(memberId);
            if (hasActive)
                throw new InvalidOperationException("Member already has an active subscription.");

            var price = dto.SubscriptionType switch
            {
                SubscriptionPeriod.Monthly => 100m,
                SubscriptionPeriod.Quarterly => 270m,
                SubscriptionPeriod.Yearly => 999m,
                _ => throw new ArgumentException("Invalid subscription type.")
            };

            var newSub = _mapper.Map<Subscription>(dto);
            newSub.MemberId = memberId;
            newSub.Price = price;
            newSub.StartDate = DateTime.UtcNow;

            await _subscriptionRepository.AddAsync(newSub);
            return "Subscription added successfully.";
        }

        public async Task<string> DeleteSubscriptionAsync(int subId)
        {
            if (subId <= 0)
                throw new ArgumentException("SubscriptionId must be greater than 0.");

            var sub = await _subscriptionRepository.GetByIdAsync(subId);

            if (sub == null)
                throw new KeyNotFoundException($"Subscription with id {subId} not found.");

            await _subscriptionRepository.DeleteAsync(sub);

            return "Subscription deleted successfully.";
        }

        public async Task<List<SubscriptionsDataDto>> GetAllSubscriptionsAsync(int pageNumber, int pageSize)
        {
            var subs = await _subscriptionRepository.GetSubscriptionsAsync(pageNumber, pageSize);

            if (!subs.Any())
                return new List<SubscriptionsDataDto>();

            return _mapper.Map<List<SubscriptionsDataDto>>(subs);
        }

        public async Task<SubscriptionsDataDto> GetSubscriptionByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Id must be a positive integer.", nameof(id));

            var sub = await _subscriptionRepository.GetByIdAsync(id);

            if (sub == null)
                throw new KeyNotFoundException($"Subscription with id {id} not found.");

            return _mapper.Map<SubscriptionsDataDto>(sub);
        }

        public async Task<string> UpdateSubscriptionAsync(int subId, UpdateSubscriptionDto dto)
        {
            if (subId <= 0)
                throw new ArgumentException("SubscriptionId must be greater than 0.");

            var sub = await _subscriptionRepository.GetByIdAsync(subId);

            if (sub == null)
                throw new KeyNotFoundException($"Subscription with id {subId} not found.");

            if (dto.Price.HasValue)
                sub.Price = dto.Price.Value;

            if (dto.SubscriptionType.HasValue)
                sub.SubscriptionType = dto.SubscriptionType.Value;

            await _subscriptionRepository.UpdateAsync(sub);

            return "Subscription updated successfully.";
        }
    }
}