using MrClean.Core.Domain.Entities;

namespace MrClean.Core.Application.Common.Interfaces.Persistence
{
    public interface IUserRepository
    {
        User? GetByEmail(string email);
        void Add(User user);
    }
}
