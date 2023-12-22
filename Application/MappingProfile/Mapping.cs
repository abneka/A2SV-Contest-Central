using AutoMapper;
using Domain.Entities;
using Application.DTOs.User;
using Application.DTOs.Contest;

namespace Application.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<UserEntity, UserRequestDto>().ReverseMap()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => {
                if (srcMember is int value && value == 0)
                {
                    return false;
                }
                return srcMember != null;
            }));

            CreateMap<ContestEntity, ContestRequestDto>().ReverseMap()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => {
                if (srcMember is int value && value == 0)
                {
                    return false;
                }
                return srcMember != null;
            }));

            CreateMap<ContestEntity, ContestResponseDto>().ReverseMap()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => {
                if (srcMember is int value && value == 0)
                {
                    return false;
                }
                return srcMember != null;
            }));
        }

    }
}
