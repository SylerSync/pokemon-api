using Core.Contracts.User;
using Core.Domain.DataObjects;

namespace Core.Services.Abstractions
{
    public interface IUserService
    {
        Task<UserDto?> AuthenticateUser(UserRequest request);
        Task<UserDto> AddNewUser(UserRequest user);
    }
}
