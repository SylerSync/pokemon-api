using Core.Contracts.Pokemon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Services.Abstractions
{
    public interface IPokemonService
    {
        Task<List<PokemonDto>> GetPokemonAsync(CancellationToken cancellationToken = default);
        Task<PokemonDto> GetPokemonByIDAsync(int id, CancellationToken cancellationToken = default);
        Task<PokemonDto> EvolvePokemonAsync(PokemonDto pokemon, CancellationToken cancellationToken = default);
        Task<PokemonDto> EditPokemonAsync(PokemonDto oldPokemon, PokemonDto newPokemon, CancellationToken cancellationToken = default);
        Task AddPokemonAsync(PokemonDto pokemon, CancellationToken cancellationToken = default);
        Task DeletePokemonAsync(PokemonDto pokemon, CancellationToken cancellationToken = default);
    }
}
