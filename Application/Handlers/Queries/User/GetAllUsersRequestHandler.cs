using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using TaskManager.Application.DTOs.User;
using TaskManager.Application.IRepositories;
using TaskManager.Application.Requests.Queries.User;
using TaskManager.Application.Responses.BaseResponses;
using TaskManager.Application.Responses.User;
using TaskManager.Domain.Entities.Identity;

namespace TaskManager.Application.Handlers.Queries.User;

/// <summary>
///     هندلر دريافت همه کاربر ها
/// </summary>
public class GetAllUsersRequestHandler : IRequestHandler<GetAllUsersRequest, ApiResponse<GetAllUsersRequestResponse>>
{
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserRepository _userRepository;

    public GetAllUsersRequestHandler(IMapper mapper, IUserRepository userRepository,
        UserManager<ApplicationUser> userManager)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _userManager = userManager;
    }

    public async Task<ApiResponse<GetAllUsersRequestResponse>> Handle(GetAllUsersRequest request,
        CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllAsync(cancellationToken);

        var result = _mapper.Map<List<UserDto>>(users);

        for (var i = 0; i < users.Count; i++) // todo: performance
        {
            var roles = await _userManager.GetRolesAsync(users[i]);
            result[i].Role = roles.FirstOrDefault()!;
        }

        return new ApiResponse<GetAllUsersRequestResponse>
        {
            Status = new StatusResponse(false),
            Data = new GetAllUsersRequestResponse
            {
                Items = result
            }
        };
    }
}