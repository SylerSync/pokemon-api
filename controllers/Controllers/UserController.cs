using Core.Contracts.User;
using Core.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace controllers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        public UserController(IServiceManager serviceManager) :base(serviceManager)
        {
    
        }
        
        // Authenticate User login
        [HttpPost("authenticate")]
        public async Task<ActionResult<UserDto?>> AuthenticateUser([FromBody] UserRequest request)
        {
            if(request == null)
            {
                return BadRequest("Email and password is required.");
            }

            var userDto = await _serviceManager.UserService.AuthenticateUser(request);

            if (userDto == null)
            {
                return Unauthorized("Email or password is incorrect.");
            }

            return Ok(userDto);
        }

        // Add new user to database
        [HttpPost("newUser")]
        public async Task<ActionResult> AddNewUser([FromBody] UserRequest request) {
            try
            {
                var newUser = await _serviceManager.UserService.AddNewUser(request);
                if (newUser is null)
                {
                    return BadRequest("User creation failed or user exists");
                }
                return Ok("New user added.");

            }
            catch (DbUpdateException)
            {
                return Conflict("A user with this email address already exists");
            }
            
        }

        // Add to user's wishList
        public record WishListRequest(string PokemonName, string User);

        [HttpPost("newWishList")]
        public async Task<ActionResult<UserDto?>> AddWishListPokemon([FromBody] WishListRequest request)
        {
            try
            {
                var updatedUser = await _serviceManager.UserService.AddWishToList(request.PokemonName, request.User);
                return Ok(updatedUser);
            }
            catch (DbUpdateException)
            {
                return BadRequest("An error occured updating user");
            }
        }

        [HttpPost("removeWishList")]
        public async Task<ActionResult<UserDto?>> RemoveWishListPokemon([FromBody] WishListRequest request)
        {
            try
            {
                var updatedUser = await _serviceManager.UserService.RemoveWishFromList(request.PokemonName, request.User);
                return Ok(updatedUser);
            }
            catch (DbUpdateException)
            {
                return BadRequest("An error occured updating user");
            }
        }

        [HttpGet("getUserData/{email}")]
        public async Task<ActionResult> GetUserData(string email)
        {
            var user = await _serviceManager.UserService.GetUserDataByEmail(email);
            if(user == null)
            {
                return BadRequest("Unable to find user data.");
            }
            return Ok(user);
        }

    }
}
