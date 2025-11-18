using FluentValidation;
using MediatR;
using TaskManager.Application.IRepositories;
using TaskManager.Application.Requests.Commands.User;
using TaskManager.Application.Responses.BaseResponses;
using TaskManager.Application.Responses.User;

namespace TaskManager.Application.Handlers.Commands.User;

/// <summary>
///     هندلر ويرايش کاربر
/// </summary>
public class UpdateUserRequestHandler : IRequestHandler<UpdateUserRequest, ApiResponse<UpdateUserRequestResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<UpdateUserRequest> _validator;

    public UpdateUserRequestHandler(IUserRepository userRepository, IValidator<UpdateUserRequest> validator)
    {
        _userRepository = userRepository;
        _validator = validator;
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

        var todo = await _userRepository.GetAsync(request.Id, cancellationToken);

        todo.Update(request.NationalCode, request.IsAdmin, request.FirstName, request.LastName, request.IsBlocked,
            request.IsActive, request.ParentRef, request.Gender);

        await _userRepository.UpdateAsync(todo, cancellationToken);

        return new ApiResponse<UpdateUserRequestResponse>
        {
            Status = new StatusResponse(false),
            Data = null
        };
    }
}