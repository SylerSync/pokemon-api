using Core.Contracts.User;
using Core.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace controllers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public UserController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<UserDto?>> AuthenticateUser([FromBody] UserRequest request)
        {
            if(request == null)
            {
                return BadRequest("Email and password is required.");
            }

            var userDto = await _serviceManager.IUserService.AuthenticateUser(request.Email, request.Password);

            return Ok(userDto);
        }


    }
}
