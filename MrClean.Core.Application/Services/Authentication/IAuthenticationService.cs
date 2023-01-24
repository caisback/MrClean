using ErrorOr;
using MrClean.Core.Application.Authentication.Common;

namespace MrClean.Core.Application.Services.Authentication
{
    public interface IAuthenticationService
    {
        ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password);
        ErrorOr<AuthenticationResult> Login (string email, string password);
    }
}
