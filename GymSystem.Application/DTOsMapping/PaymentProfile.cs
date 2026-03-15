using AutoMapper;
using GymSystem.Application.Dtos.Payment;
using GymSystem.Domain.Entities;

namespace GymSystem.Application.DTOsMapping
{
    public class PaymentProfile : Profile
    {
        public PaymentProfile()
        {
            CreateMap<CreatePaymentDto, Payment>()
                .ForMember(dest => dest.Member, opt => opt.Ignore())
                .ForMember(dest => dest.Subscription, opt => opt.Ignore())
                .ForMember(dest => dest.MemberId, opt => opt.Ignore());

            CreateMap<Payment, PaymentResponseDto>()
                .ForMember(dest => dest.MemberName, opt => opt.MapFrom(src => src.Member.FullName))
                .ForMember(dest => dest.SubscriptionType, opt => opt.MapFrom(src => src.Subscription.SubscriptionType));
        }
    }
}
