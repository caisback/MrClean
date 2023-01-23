using MrClean.Core.Application.Common.Interfaces.Persistence;
using MrClean.Core.Domain.Entities;

namespace MrClean.Infrastructure.Persistence
{
    public class UserRepository : IUserRepository
    {
        private static readonly List<User> _users = new();
        void IUserRepository.Add(User user)
        {
            _users.Add(user);
        }

        User? IUserRepository.GetByEmail(string email)
        {
            return _users.SingleOrDefault(u => u.Email == email);
        }
    }
}
