using AutoMapper;
using GymSystem.Application.Abstracts;
using GymSystem.Application.Dtos.Payment;
using GymSystem.Application.Interfaces;
using GymSystem.Infrastructure.Interfaces;

namespace GymSystem.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IMapper _mapper;

        public PaymentService(IPaymentRepository paymentRepository, IMemberRepository memberRepository,
            ISubscriptionRepository subscriptionRepository, IMapper mapper)
        {
            _paymentRepository = paymentRepository;
            _memberRepository = memberRepository;
            _subscriptionRepository = subscriptionRepository;
            _mapper = mapper;
        }

        public async Task<List<PaymentResponseDto>> GetAllPaymentsAsync(int pageNumber, int pageSize)
        {
            var payments = await _paymentRepository.GetAllPaymentsAsync(pageNumber, pageSize);
            if (!payments.Any())
                return new List<PaymentResponseDto>();
            return _mapper.Map<List<PaymentResponseDto>>(payments);
        }

        public async Task<List<PaymentResponseDto>> GetMemberPaymentsAsync(int memberId, string CurrentUserId, string role)
        {
            if (memberId <= 0)
                throw new ArgumentException("Enter Valid Mmber Id.");
            var member = await _memberRepository.GetByIdAsync(memberId);
            if (member == null)
                throw new KeyNotFoundException($"Member with id {memberId} not found.");

            switch (role)
            {
                case "Member":
                    if (member.ApplicationUserId != CurrentUserId)
                        throw new UnauthorizedAccessException("You can only view your own data.");
                    break;
                case "Admin":
                    break;

                default:
                    throw new UnauthorizedAccessException("Unknown role.");
            }
            var payments = await _paymentRepository.GetByMemberIdAsync(memberId);
            if (!payments.Any())
                return new List<PaymentResponseDto>();
            return _mapper.Map<List<PaymentResponseDto>>(payments);
        }

        public async Task<PaymentResponseDto> GetPaymentByIdAsync(int paymentId)
        {
            if (paymentId <= 0)
                throw new ArgumentException("Enter Valid Payment Id.");
            var payment = await _paymentRepository.GetByIdAsync(paymentId);
            if (payment == null)
                throw new KeyNotFoundException($"Payment with id {paymentId} not found.");

            return _mapper.Map<PaymentResponseDto>(payment);
        }

        public async Task<string> ProcessPaymentAsync(string currentUserId, CreatePaymentDto dto)
        {
            var member = await _memberRepository.GetByApplicationUserIdAsync(currentUserId);
            if (member == null)
                throw new KeyNotFoundException("Member not found.");

            var subscription = await _subscriptionRepository.GetByIdAsync(dto.SubscriptionId);
            if (subscription == null)
                throw new KeyNotFoundException($"Subscription with id {dto.SubscriptionId} not found.");

            // ✅ دلوقتي بتتأكد إن الـ Subscription بتاعت الـ Member اللي logged in فعلاً
            if (subscription.MemberId != member.Id)
                throw new UnauthorizedAccessException("This subscription does not belong to you.");

            var alreadyPaid = await _paymentRepository.ExistsAsync(member.Id, dto.SubscriptionId);
            if (alreadyPaid)
                throw new InvalidOperationException("This subscription has already been paid.");

            var payment = new GymSystem.Domain.Entities.Payment
            {
                MemberId = member.Id,
                SubscriptionId = dto.SubscriptionId,
                PaymentMethod = dto.PaymentMethod,
                Amount = subscription.Price,
                PaymentDate = DateTime.UtcNow,
                IsSuccessful = true
            };

            await _paymentRepository.AddAsync(payment);
            return $"Payment of {subscription.Price} EGP processed successfully.";
        }
    }
}
