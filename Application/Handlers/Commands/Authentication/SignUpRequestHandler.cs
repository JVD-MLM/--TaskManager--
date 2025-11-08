using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using TaskManager.Application.Requests.Commands.Authentication;
using TaskManager.Application.Responses;
using TaskManager.Application.Responses.Authentication;
using TaskManager.Application.Responses.BaseResponses;
using TaskManager.Domain.Entities.Identity;

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
                Status = new StatusResponse
                {
                    Message = "حساب کاربری ساخته نشد",
                    HasError = true
                },
                Data = null
            };

        var user = _mapper.Map<ApplicationUser>(request);

        var createUser = await _userManager.CreateAsync(user, request.Password);

        if (createUser.Succeeded)
        {
            var roleExists = await _userManager.IsInRoleAsync(user, "Employee");

            if (!roleExists) await _userManager.AddToRoleAsync(user, "Employee");

            return new ApiResponse<SignUpRequestResponse>
            {
                Status = new StatusResponse
                {
                    Message = "حساب کاربری با موفقیت ساخته شد"
                },
                Data = null
            };
        }

        return new ApiResponse<SignUpRequestResponse>
        {
            Status = new StatusResponse
            {
                Message = "حساب کاربری ساخته نشد",
                HasError = true
            },
            Data = null
        };
    }
}