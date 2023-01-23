using MrClean.Core.Domain.Entities;

namespace MrClean.Core.Application.Common.Interfaces.Authentication
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
