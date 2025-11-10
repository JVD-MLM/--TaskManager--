using AutoMapper;
using TaskManager.Application.DTOs.Project;
using TaskManager.Application.Requests.Commands.Authentication;
using TaskManager.Application.Requests.Commands.Project;
using TaskManager.Domain.Entities.Identity;
using TaskManager.Domain.Entities.Project;

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

        CreateMap<Project, CreateProjectRequest>().ReverseMap()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));

        CreateMap<Project, ProjectDto>().ReverseMap();
    }
}