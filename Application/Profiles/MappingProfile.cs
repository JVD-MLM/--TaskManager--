using AutoMapper;
using TaskManager.Application.DTOs.Project;
using TaskManager.Application.Requests.Commands.Authentication;
using TaskManager.Application.Requests.Commands.Project;
using TaskManager.Application.Requests.Commands.Todo;
using TaskManager.Domain.Entities.Identity;
using TaskManager.Domain.Entities.Project;
using TaskManager.Domain.Entities.Todo;

namespace TaskManager.Application.Profiles;

/// <summary>
///     پروفایل مپ AutoMapper
/// </summary>
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        #region ApplicationUser

        CreateMap<ApplicationUser, SignUpRequest>().ReverseMap()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));

        #endregion

        #region Project

        CreateMap<Project, CreateProjectRequest>().ReverseMap()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));

        CreateMap<Project, ProjectDto>().ReverseMap();

        #endregion

        #region Todo

        CreateMap<Todo, CreateTodoRequest>().ReverseMap()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.DeadLine, opt => opt.MapFrom(src => src.DeadLine))
            .ForMember(dest => dest.NeedApprove, opt => opt.MapFrom(src => src.NeedApprove))
            .ForMember(dest => dest.ProjectRef, opt => opt.MapFrom(src => src.ProjectRef));

        #endregion
    }
}