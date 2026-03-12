using AutoMapper;
using GymSystem.Application.Dtos.Auth;
using GymSystem.Application.Dtos.MemberDto;
using GymSystem.Application.Dtos.SubscriptionDto;
using GymSystem.Domain.Entities;

namespace GymSystem.Application.DTOsMapping
{
    public class MemberProfile : Profile
    {
        public MemberProfile()
        {
            CreateMap<Member, MembersDataDto>()
                    .ForMember(d => d.CoachName, opt => opt.MapFrom(s => s.FullName))
                    .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                    .ForMember(dest => dest.MemberType, opt => opt.MapFrom(src => src.MemberType.ToString()));
            CreateMap<CreateMemberDto, Member>();
            CreateMap<UpdateMemberDto, Member>()
                    .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<RegisterDto, Member>()
                    .ForMember(d => d.PhoneNumber, opt => opt.MapFrom(s => s.PhoneNumber))
                    .ForMember(d => d.ApplicationUserId, opt => opt.Ignore())
                    .ForMember(d => d.Id, opt => opt.Ignore());
            CreateMap<Member, MemberSubscriptionsDto>()
                .ForMember(dest => dest.MemberId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.MemberName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.Subscriptions, opt => opt.MapFrom(src => src.Subscriptions));

            CreateMap<Subscription, SubscriptionsDataDto>();
        }
    }
}
