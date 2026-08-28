using Core.Domain.DataObjects;
using Core.Domain.Repositories.Abstactions;
using Microsoft.EntityFrameworkCore;

namespace Core.Infrastructure.Repositories
{
    internal sealed class UserRepository : RepositoryBase, IUserRepository
    {
        public UserRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

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
