using ErrorOr;
using MediatR;
using MrClean.Core.Application.Authentication.Common;

namespace MrClean.Core.Application.Authentication.Queries.Login
{
    public record LoginQuery(
        string Email,
        string Password) : IRequest<ErrorOr<AuthenticationResult>>;
}
