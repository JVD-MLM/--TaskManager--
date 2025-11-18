using MediatR;
using TaskManager.Application.Responses.BaseResponses;
using TaskManager.Application.Responses.User;

namespace TaskManager.Application.Requests.Queries.User;

/// <summary>
///     درخواست دريافت کاربر با کد ملی
/// </summary>
public class GetUserByNationalCodeRequest : IRequest<ApiResponse<GetUserByNationalCodeRequestResponse>>
{
    /// <summary>
    ///     کد ملی
    /// </summary>
    public string NationalCode { get; set; }
}