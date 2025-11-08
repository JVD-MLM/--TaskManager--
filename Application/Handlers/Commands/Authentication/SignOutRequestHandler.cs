using System.IdentityModel.Tokens.Jwt;
using MediatR;
using Microsoft.AspNetCore.Identity;
using TaskManager.Application.IRepositories;
using TaskManager.Application.Requests.Commands.Authentication;
using TaskManager.Application.Responses;
using TaskManager.Application.Responses.Authentication;
using TaskManager.Domain.Entities.Identity;
using TaskManager.Domain.Entities.Jwt;

namespace TaskManager.Application.Handlers.Commands.Authentication;

/// <summary>
///     هندلر خروج کاربر
/// </summary>
public class SignOutRequestHandler : IRequestHandler<SignOutRequest, ApiResponse<SignOutRequestResponse>>
{
    private readonly IJwtRepository _jwtRepository;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public SignOutRequestHandler(SignInManager<ApplicationUser> signInManager, IJwtRepository jwtRepository)
    {
        _signInManager = signInManager;
        _jwtRepository = jwtRepository;
    }

    public async Task<ApiResponse<SignOutRequestResponse>> Handle(SignOutRequest request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Token))
            return new ApiResponse<SignOutRequestResponse>
            {
                Status = new StatusResponse
                {
                    Message = "توکن معتبر ارسال نشده است.",
                    HasError = true
                },
                Data = null
            };

        var token = request.Token.TrimStart();

        if (token.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase)) token = token.Substring(7).Trim();

        var tokenHandler = new JwtSecurityTokenHandler();
        DateTime expireAt;

        try
        {
            var jwtToken = tokenHandler.ReadJwtToken(token);
            expireAt = jwtToken.ValidTo.ToUniversalTime();
        }
        catch
        {
            expireAt = DateTime.UtcNow.AddHours(1);
        }

        var revokedToken = new RevokedToken
        {
            Token = token,
            ExpireAt = expireAt
        };

        await _jwtRepository.RevokeToken(revokedToken);

        await _signInManager.SignOutAsync();

        return new ApiResponse<SignOutRequestResponse>
        {
            Status = new StatusResponse
            {
                Message = "با موفقیت خارج شدید"
            },
            Data = null
        };
    }
}