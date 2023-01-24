using MrClean.Core.Domain.Entities;

namespace MrClean.Core.Application.Authentication.Common
{
    public record AuthenticationResult(
        User User,
        string Token);
}
