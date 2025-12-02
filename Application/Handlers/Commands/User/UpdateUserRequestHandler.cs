using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using TaskManager.Application.IRepositories;
using TaskManager.Application.Requests.Commands.User;
using TaskManager.Application.Responses.BaseResponses;
using TaskManager.Application.Responses.User;
using TaskManager.Domain.Entities.Identity;
using TaskManager.Domain.Enums.Extensions;

namespace TaskManager.Application.Handlers.Commands.User;

/// <summary>
///     هندلر ويرايش کاربر
/// </summary>
public class UpdateUserRequestHandler : IRequestHandler<UpdateUserRequest, ApiResponse<UpdateUserRequestResponse>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserRepository _userRepository;
    private readonly IValidator<UpdateUserRequest> _validator;

    public UpdateUserRequestHandler(IUserRepository userRepository, IValidator<UpdateUserRequest> validator,
        UserManager<ApplicationUser> userManager)
    {
        _userRepository = userRepository;
        _validator = validator;
        _userManager = userManager;
    }

    public async Task<ApiResponse<UpdateUserRequestResponse>> Handle(UpdateUserRequest request,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return new ApiResponse<UpdateUserRequestResponse>
            {
                Status = new StatusResponse(true)
                {
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                },
                Data = null
            };

        var user = await _userRepository.GetAsync(request.Id, cancellationToken);

        if (!string.IsNullOrEmpty(request.Password))
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var result = await _userManager.ResetPasswordAsync(user, token, request.Password);

            if (!result.Succeeded)
                return new ApiResponse<UpdateUserRequestResponse>
                {
                    Status = new StatusResponse(true)
                    {
                        Errors = result.Errors.Select(e => e.Description).ToList() // ارور هاي identity
                    },
                    Data = null
                };
        }

        user.Update(request.NationalCode, request.IsAdmin, request.FirstName, request.LastName, request.IsBlocked,
            request.IsActive, request.ParentRef, request.Gender);

        if (!string.IsNullOrEmpty(request.NationalCode)) user.UserName = request.NationalCode;

        var currentRoles = await _userManager.GetRolesAsync(user);

        if (currentRoles.Any())
            await _userManager.RemoveFromRolesAsync(user, currentRoles);

        await _userManager.AddToRoleAsync(user, request.Role.GetName());

        await _userRepository.UpdateAsync(user, cancellationToken);

        return new ApiResponse<UpdateUserRequestResponse>
        {
            Status = new StatusResponse(false),
            Data = null
        };
    }
}