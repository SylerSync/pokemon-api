using Core.Domain.DataObjects;
using Core.Services.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace Core.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly IPasswordHasher<User> _passwordHasher;

        public PasswordService(IPasswordHasher<User> passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }
        public string HashPassword(User user, string plainTextPassord)
        {
            return _passwordHasher.HashPassword(user, plainTextPassord);
        }

        public bool VerifyPassword(User user, string providedPassword)
        {
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, providedPassword);
            return result != PasswordVerificationResult.Failed;
        }
    }
}
