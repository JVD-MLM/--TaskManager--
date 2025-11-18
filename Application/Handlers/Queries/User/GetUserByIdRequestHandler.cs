using AutoMapper;
using FluentValidation;
using MediatR;
using TaskManager.Application.DTOs.User;
using TaskManager.Application.IRepositories;
using TaskManager.Application.Requests.Queries.User;
using TaskManager.Application.Responses.BaseResponses;
using TaskManager.Application.Responses.User;

namespace TaskManager.Application.Handlers.Queries.User;

/// <summary>
///     هندلر دريافت کاربر با آی دی
/// </summary>
public class GetUserByIdRequestHandler : IRequestHandler<GetUserByIdRequest, ApiResponse<GetUserByIdRequestResponse>>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly IValidator<GetUserByIdRequest> _validator;

    public GetUserByIdRequestHandler(IMapper mapper, IUserRepository userRepository,
        IValidator<GetUserByIdRequest> validator)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _validator = validator;
    }

    public async Task<ApiResponse<GetUserByIdRequestResponse>> Handle(GetUserByIdRequest request,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return new ApiResponse<GetUserByIdRequestResponse>
            {
                Status = new StatusResponse(true)
                {
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                },
                Data = null
            };

        var user = await _userRepository.GetAsync(request.Id, cancellationToken);

        var result = _mapper.Map<UserDto>(user);

        return new ApiResponse<GetUserByIdRequestResponse>
        {
            Status = new StatusResponse(false),
            Data = new GetUserByIdRequestResponse
            {
                Item = result
            }
        };
    }
}