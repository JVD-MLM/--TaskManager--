using AutoMapper;
using FluentValidation;
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
///     هندلر دريافت کاربر با آی دی
/// </summary>
public class GetUserByIdRequestHandler : IRequestHandler<GetUserByIdRequest, ApiResponse<GetUserByIdRequestResponse>>
{
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserRepository _userRepository;
    private readonly IValidator<GetUserByIdRequest> _validator;

    public GetUserByIdRequestHandler(IMapper mapper, IUserRepository userRepository,
        IValidator<GetUserByIdRequest> validator, UserManager<ApplicationUser> userManager)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _validator = validator;
        _userManager = userManager;
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
                Result = null
            };

        var user = await _userRepository.GetAsync(request.Id, cancellationToken);

        var result = _mapper.Map<UserDto>(user);

        var roles = await _userManager.GetRolesAsync(user);

        result.Role = roles.FirstOrDefault()!;

        return new ApiResponse<GetUserByIdRequestResponse>
        {
            Status = new StatusResponse(false),
            Result = new GetUserByIdRequestResponse
            {
                Item = result
            }
        };
    }
}