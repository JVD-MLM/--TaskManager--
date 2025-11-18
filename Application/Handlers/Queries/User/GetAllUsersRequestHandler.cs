using AutoMapper;
using MediatR;
using TaskManager.Application.DTOs.User;
using TaskManager.Application.IRepositories;
using TaskManager.Application.Requests.Queries.User;
using TaskManager.Application.Responses.BaseResponses;
using TaskManager.Application.Responses.User;

namespace TaskManager.Application.Handlers.Queries.User;

/// <summary>
///     هندلر دريافت همه کاربر ها
/// </summary>
public class GetAllUsersRequestHandler : IRequestHandler<GetAllUsersRequest, ApiResponse<GetAllUsersRequestResponse>>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public GetAllUsersRequestHandler(IMapper mapper, IUserRepository userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<ApiResponse<GetAllUsersRequestResponse>> Handle(GetAllUsersRequest request,
        CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllAsync(cancellationToken);

        var result = _mapper.Map<List<UserDto>>(users);

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