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

        // Fetch user document by normalized email
        public async Task<User?> GetUserByEmail(string email)
        {
            var normalizedEmail = email.Trim().ToLowerInvariant();
            return await _dbContext.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Email == normalizedEmail);
        }

        // Add a new User entity into the users collection
        public async Task<bool> InsertNewUser(User newUser)
        {
            try
            {
                // 1. Stage the entity
                await _dbContext.Users.AddAsync(newUser);

                // 2. Save directly without BeginTransactionAsync
                int documentsAffected = await _dbContext.SaveChangesAsync();

                return documentsAffected == 1;
            }
            catch (DbUpdateException)
            {
                // Handles primary key (Email) duplicates or database constraint errors
                return false;
            }
        }
    }
}
