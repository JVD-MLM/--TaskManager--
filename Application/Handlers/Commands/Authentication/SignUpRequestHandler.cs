using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using TaskManager.Application.Requests.Commands.Authentication;
using TaskManager.Application.Responses.Authentication;
using TaskManager.Application.Responses.BaseResponses;
using TaskManager.Domain.Entities.Identity;
using TaskManager.Domain.Enums.Extensions;

namespace TaskManager.Application.Handlers.Commands.Authentication;

/// <summary>
///     هندلر ثبت نام کاربر
/// </summary>
public class SignUpRequestHandler : IRequestHandler<SignUpRequest, ApiResponse<SignUpRequestResponse>>
{
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IValidator<SignUpRequest> _validator;

    public SignUpRequestHandler(IMapper mapper, UserManager<ApplicationUser> userManager,
        IValidator<SignUpRequest> validator)
    {
        _mapper = mapper;
        _userManager = userManager;
        _validator = validator;
    }

    public async Task<ApiResponse<SignUpRequestResponse>> Handle(SignUpRequest request,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return new ApiResponse<SignUpRequestResponse>
            {
                Status = new StatusResponse(true)
                {
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList() // ارور هاي validation
                },
                Result = null
            };

        var user = _mapper.Map<ApplicationUser>(request);

        user.UserName = request.NationalCode; // برای اینکه UserName خالی نباشد

        var createUser = await _userManager.CreateAsync(user, request.Password);

        if (createUser.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, request.Role.GetName());

            return new ApiResponse<SignUpRequestResponse>
            {
                Status = new StatusResponse(false),
                Result = null
            };
        }

        return new ApiResponse<SignUpRequestResponse>
        {
            Status = new StatusResponse(true)
            {
                Errors = createUser.Errors.Select(e => e.Description).ToList() // ارور هاي identity
            },
            Result = null
        };
    }
}