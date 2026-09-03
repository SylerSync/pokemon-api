using Core.Contracts.PokeBox;
using Core.Contracts.Pokemon;
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

        [HttpPost("removePokemonFromBox")]
        public async Task<ActionResult<PokeBoxDto?>> RemoveFromPokeBox([FromBody] PokeBoxRequest request)
        {
            try
            {
                var updatedBox = await _serviceManager.PokeBoxService.RemoveFromUsersPokeBox(request.UserID, request.Pokemon);
                return Ok(updatedBox);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured updating pokebox");
            }
        }

        [HttpPost("updateCaughtPokemon")]
        public async Task<ActionResult<PokemonFullInfoDto?>> UpdateCaughtPokemon([FromBody] PokeBoxRequest request)
        {
            try
            {
                var updatedPokemon = await _serviceManager.PokeBoxService.UpdateCaughtPokemon(request.UserID, request.Pokemon);
                return Ok(updatedPokemon);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured updating caught pokemon");
            }
        }

        [HttpGet("getPokeBox/{userID}")]
        public async Task<ActionResult<PokeBoxDto?>> GetPokeBox(string userID)
        {
            try
            {
                var pokeBox = await _serviceManager.PokeBoxService.GetPokeBox(userID);
                if (pokeBox == null)
                {
                    return NotFound($"PokeBox for user {userID} not found.");
                }
                return Ok(pokeBox);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred while retrieving the PokeBox.");
            }
        }
    }
}
