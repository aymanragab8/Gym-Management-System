using AutoMapper;
using GymSystem.Application.Dtos.SubscriptionDto;
using GymSystem.Domain.Entities;

namespace GymSystem.Application.DTOsMapping
{
    public class SubscriptionProfile : Profile
    {
        public SubscriptionProfile()
        {
            CreateMap<Subscription, SubscriptionsDataDto>()
                .ForMember(dest => dest.MemberName, opt => opt.MapFrom(src => src.Member.FullName));

            CreateMap<CreateSubscriptionDto, Subscription>()
                .ForMember(dest => dest.Member, opt => opt.Ignore())
                .ForMember(dest => dest.Payments, opt => opt.Ignore())
                .ForMember(dest => dest.MemberId, opt => opt.Ignore());

            CreateMap<UpdateSubscriptionDto, Subscription>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }

    }
}
