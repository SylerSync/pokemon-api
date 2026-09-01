using Core.Domain.DataObjects;

namespace Core.Domain.Repositories.Abstactions
{
    public interface IUserRepository
    {
        // Authenticate function is not needed due to AspNetCore.Identity handling password validation in service layer.
        Task<User?> GetUserByEmail(string email);
        Task<bool> InsertNewUser(User user);
        Task<bool> AddToWishListAsync(string pokemonName, string user, CancellationToken cancellationToken = default);
        Task<bool> RemoveFromWishListAsync(string pokemonName, string user, CancellationToken cancellationToken = default);
    }
}
