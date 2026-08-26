using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace controllers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        //private readonly IServiceManager _serviceManager;

        //public PokemonController(IServiceManager serviceManager) => _serviceManager = serviceManager;

        // GET: api/<PokemonController>
        [HttpGet]
        public IEnumerable<string> GetPokemon()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<PokemonController>/5
        [HttpGet("{id}")]
        public string GetPokemonByID(int id)
        {
            return "value";
        }

        // POST api/<PokemonController>
        [HttpPost]
        public void AddPokemon([FromBody] string value)
        {
        }

        [HttpPost("{id}/evolve")]
        public void EvolvePokemon([FromBody] string value) 
        {
        }

        // PUT api/<PokemonController>/5
        [HttpPut("{id}")]
        public void UpdatePokemon(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PokemonController>/5
        [HttpDelete("{id}")]
        public void DeletePokemon(int id)
        {
        }
    }
}
