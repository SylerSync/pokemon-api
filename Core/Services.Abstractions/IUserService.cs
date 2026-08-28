using Core.Contracts.User;

namespace Core.Services.Abstractions
{
    public interface IUserService
    {
        Task<UserDto?> AuthenticateUser(string email, string passwordHash);
    }
}
