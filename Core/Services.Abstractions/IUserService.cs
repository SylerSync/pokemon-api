using Core.Contracts.User;
using Core.Domain.DataObjects;

namespace Core.Services.Abstractions
{
    public interface IUserService
    {
        Task<UserDto?> AuthenticateUser(UserRequest request);
        Task<UserDto> AddNewUser(UserRequest user);
        Task<UserDto> AddWishToList(string pokemonName, string user, CancellationToken cancellationToken = default);
        Task<UserDto> RemoveWishFromList(string pokemonName, string user, CancellationToken cancellationToken = default);
        Task<UserDto?> GetUserDataByEmail(string email);
    }
}
