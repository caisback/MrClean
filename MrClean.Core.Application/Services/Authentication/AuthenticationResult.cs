using MrClean.Core.Domain.Entities;

namespace MrClean.Core.Application.Services.Authentication
{
    public record AuthenticationResult(
        User User,
        string Token);
}
