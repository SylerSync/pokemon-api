using Core.Domain.DataObjects;

namespace Core.Services.Abstractions
{
    public interface IPasswordService
    {
        string HashPassword(User user, string plainTextPassord);
        bool VerifyPassword(User user, string providedPAssword);
    }
}
