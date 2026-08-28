using Core.Domain.DataObjects;
using Core.Domain.Repositories.Abstactions;

namespace Core.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        public Task<bool> AuthenticateUser(string email, string passwordHash)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }
    }
}
