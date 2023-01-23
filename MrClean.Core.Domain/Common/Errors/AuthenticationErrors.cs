using ErrorOr;

namespace MrClean.Core.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class AuthenticationErrors
        {
            public static Error InvalidCredentials => Error.Validation(code: "Auth.InvalidCredentials", description: "Invalid credentials.");
        }
    }
}
