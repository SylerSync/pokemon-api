using Core.Contracts.PokeBox;
using Core.Contracts.User;
using Core.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static controllers.Controllers.UserController;

namespace controllers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokeBoxController : BaseController
    {
        public PokeBoxController(IServiceManager serviceManager) : base(serviceManager)
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("addPokemonToBox")]
        public async Task<ActionResult<PokeBoxDto?>> AddToPokeBox([FromBody] PokeBoxRequest request)
        {
            try
            {
                var updatedBox = await _serviceManager.PokeBoxService.AddToUsersPokeBox(request.UserID, request.Pokemon);
                return Ok(updatedBox);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured updating pokebox");
            }
        }
    }
}
