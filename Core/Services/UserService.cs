using Core.Contracts.User;
using Core.Domain.DataObjects;
using Core.Domain.Repositories.Abstactions;
using Core.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services
{
    internal sealed class UserService : ServiceBase, IUserService
    {

        public UserService(IRepositoryManager repositoryManager) : base(repositoryManager)
        {
        
        }

        //Authenticate user and return UserDto
        public async Task<UserDto?> AuthenticateUser(string email, string passwordHash)
        { 
            if(await _repositoryManager.UserRepository.AuthenticateUser(email, passwordHash))
            {
                var user = await _repositoryManager.UserRepository.GetUserByEmail(email);
                if (user != null) // ensure the user hasnt been deleted since auth call
                {
                    return MapToDto(user);
                }
                return null; // User couldnt be found after authentication
            }
            return null; // Authentication failed
        }

        //DTO Mapping
        public UserDto MapToDto(User user)
        {
            var dto = new UserDto
            {
                Email = user.Email,
                Username = user.Username,
            };

            return dto;
        }

    }
}
