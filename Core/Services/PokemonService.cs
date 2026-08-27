using Core.Contracts.Pokemon;
using Core.Domain.Repositories.Abstactions;
using Core.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Services
{
    internal sealed class PokemonService : IPokemonService
    {
        private readonly IPokemonRepository _pokemonRepository;

        public PokemonService(IPokemonRepository pokemonRepository)
        {
            _pokemonRepository = pokemonRepository;
        }

        public Task AddPokemonAsync(PokemonDto pokemon, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task DeletePokemonAsync(PokemonDto pokemon, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<PokemonDto> EditPokemonAsync(PokemonDto oldPokemon, PokemonDto newPokemon, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<PokemonDto> EvolvePokemonAsync(PokemonDto pokemon, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<PokemonDto> GetPokemonByIDAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<List<PokemonDto>> GetPokemonListAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
