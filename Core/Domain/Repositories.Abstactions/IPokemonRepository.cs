using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Domain.DataObjects;

namespace Core.Domain.Repositories.Abstactions
{
    public interface IPokemonRepository
    {
        Task<List<Pokemon>> GetPokemonAsync(CancellationToken cancellationToken = default);
        Task<Pokemon> GetPokemonByIDAsync(int id, CancellationToken cancellationToken = default);
        Task<Pokemon> EvolvePokemonAsync(Pokemon pokemon, CancellationToken cancellationToken = default);
        Task<Pokemon> EditPokemonAsync(Pokemon oldPokemon, Pokemon newPokemon, CancellationToken cancellationToken = default);
        Task AddPokemonAsync(Pokemon pokemon, CancellationToken cancellationToken = default);
        Task DeletePokemonAsync(Pokemon pokemon, CancellationToken cancellationToken = default);

    }
}
