using ErrorOr;
using MediatR;
using MrClean.Core.Application.Authentication.Common;

namespace MrClean.Core.Application.Authentication.Commands.Register
{
    public record RegisterCommand(
        string FirstName,
        string LastName,
        string Email,
        string Password) : IRequest<ErrorOr<AuthenticationResult>>;
}
