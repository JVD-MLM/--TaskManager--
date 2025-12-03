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
///     هندلر دريافت کاربر با هندلر
/// </summary>
public class GetUserByNationalCodeRequestHandler : IRequestHandler<GetUserByNationalCodeRequest,
    ApiResponse<GetUserByNationalCodeRequestResponse>>
{
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserRepository _userRepository;
    private readonly IValidator<GetUserByNationalCodeRequest> _validator;

    public GetUserByNationalCodeRequestHandler(IMapper mapper, IUserRepository userRepository,
        IValidator<GetUserByNationalCodeRequest> validator, UserManager<ApplicationUser> userManager)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _validator = validator;
        _userManager = userManager;
    }

    public async Task<ApiResponse<GetUserByNationalCodeRequestResponse>> Handle(GetUserByNationalCodeRequest request,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return new ApiResponse<GetUserByNationalCodeRequestResponse>
            {
                Status = new StatusResponse(true)
                {
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                },
                Result = null
            };

        var user = await _userRepository.GetAsync(request.NationalCode, cancellationToken);

        var result = _mapper.Map<UserDto>(user);

        var roles = await _userManager.GetRolesAsync(user);

        result.Role = roles.FirstOrDefault()!;

        return new ApiResponse<GetUserByNationalCodeRequestResponse>
        {
            Status = new StatusResponse(false),
            Result = new GetUserByNationalCodeRequestResponse
            {
                Item = result
            }
        };
    }
}