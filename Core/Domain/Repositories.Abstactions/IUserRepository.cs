using Core.Domain.DataObjects;

namespace Core.Domain.Repositories.Abstactions
{
    public interface IUserRepository
    {
        Task<bool> AuthenticateUser(string email, string passwordHash);
        Task<User> GetUserByEmail(string email);
    }
}
