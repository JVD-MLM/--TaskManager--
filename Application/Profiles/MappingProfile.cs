using AutoMapper;
using TaskManager.Application.Requests.Commands.Authentication;
using TaskManager.Domain.Entities.Identity;

namespace TaskManager.Application.Profiles;

/// <summary>
///     پروفایل مپ AutoMapper
/// </summary>
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ApplicationUser, SignUpRequest>().ReverseMap()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));
    }
}