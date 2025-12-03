using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TaskManager.Application.Requests.Commands.Authentication;
using TaskManager.Application.Responses.Authentication;
using TaskManager.Application.Responses.BaseResponses;
using TaskManager.Domain.Entities.Identity;

namespace TaskManager.Application.Handlers.Commands.Authentication;

/// <summary>
///     هندلر ورود کاربر
/// </summary>
public class SignInRequestHandler : IRequestHandler<SignInRequest, ApiResponse<SignInRequestResponse>>
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<ApplicationUser> _userManager;

    public SignInRequestHandler(UserManager<ApplicationUser> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<ApiResponse<SignInRequestResponse>> Handle(SignInRequest request,
        CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.NationalCode == request.NationalCode,
            cancellationToken);

        if (user != null && await _userManager.CheckPasswordAsync(user, request.Password))
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Name, user.NationalCode),
                new(ClaimTypes.Role, userRoles.FirstOrDefault()),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var authSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:DurationInMinutes"])),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return new ApiResponse<SignInRequestResponse>
            {
                Status = new StatusResponse(false),
                Result = new SignInRequestResponse
                {
                    NationalCode = user.NationalCode,
                    FirstName = user.FirstName ?? "",
                    LastName = user.LastName ?? "",
                    RoleName = userRoles.FirstOrDefault() ?? "",
                    ExpiredAt = $"{token.ValidTo}",
                    Token = "Bearer " + new JwtSecurityTokenHandler().WriteToken(token)
                }
            };
        }

        return new ApiResponse<SignInRequestResponse>
        {
            Status = new StatusResponse(true)
            {
                Errors = new List<string> { "کاربر یافت نشد" }
            },
            Result = null
        };
    }
}