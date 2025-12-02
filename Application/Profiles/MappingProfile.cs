using AutoMapper;
using TaskManager.Application.DTOs.Project;
using TaskManager.Application.DTOs.Todo;
using TaskManager.Application.DTOs.User;
using TaskManager.Application.Requests.Commands.Authentication;
using TaskManager.Application.Requests.Commands.Project;
using TaskManager.Application.Requests.Commands.Todo;
using TaskManager.Application.Utilities;
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
            .ForMember(dest => dest.NationalCode, opt => opt.MapFrom(src => src.NationalCode))
            .ForMember(dest => dest.ParentRef, opt => opt.MapFrom(src => src.ParentRef));

        #endregion

        #region Project

        CreateMap<Project, CreateProjectRequest>().ReverseMap()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));

        CreateMap<Project, ProjectDto>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToPersianDateTime()));

        #endregion

        #region Todo

        CreateMap<Todo, CreateTodoRequest>().ReverseMap()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.DeadLine, opt => opt.MapFrom(src => src.DeadLine))
            .ForMember(dest => dest.NeedApprove, opt => opt.MapFrom(src => src.NeedApprove))
            .ForMember(dest => dest.ProjectRef, opt => opt.MapFrom(src => src.ProjectRef));

        CreateMap<Todo, TodoDto>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToPersianDateTime()))
            .ForMember(dest => dest.DeadLine, opt => opt.MapFrom(src => src.DeadLine.ToPersianDateTime()));

        #endregion

        #region User

        CreateMap<ApplicationUser, UserDto>().ReverseMap();

        #endregion
    }
}