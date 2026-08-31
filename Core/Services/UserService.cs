using Core.Contracts.User;
using Core.Domain.DataObjects;
using Core.Domain.Repositories.Abstactions;
using Core.Services.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace Core.Services
{
    internal sealed class UserService : ServiceBase, IUserService
    {
        private readonly IPasswordHasher<User> _passwordHasher;
        public UserService(IRepositoryManager repositoryManager, IPasswordHasher<User> passwordHasher) : base(repositoryManager)
        {
            _passwordHasher = passwordHasher;
        }

        public async Task<UserDto> AddNewUser(UserRequest user)
        {
            var normalizedEmail = user.Email.Trim().ToLowerInvariant();

            var newUser = new User
            {
                Email = normalizedEmail
            };

            // Hash the plain string data.
            newUser.PasswordHash = _passwordHasher.HashPassword(newUser, user.Password);

            bool isSuccess = await _repositoryManager.UserRepository.InsertNewUser(newUser);
            if (!isSuccess) return null;

            return MapToDto(newUser);
        }

        //Authenticate user and return UserDto
        public async Task<UserDto?> AuthenticateUser(UserRequest request)
        {
            // Normalize email and check for a user
            var normalizedEmail = request.Email.Trim().ToLowerInvariant();
            var user = await _repositoryManager.UserRepository.GetUserByEmail(normalizedEmail);
            if(user is null)
            {
                return null; //No user found
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);

            if(result == PasswordVerificationResult.Failed)
            {
                return null; //Invalid password
            }

            return MapToDto(user);
            
        }

        //DTO Mapping
        public UserDto MapToDto(User user)
        {
            var dto = new UserDto
            {
                Email = user.Email
            };

            return dto;
        }

    }
}
