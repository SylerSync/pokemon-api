using Core.Contracts.Pokemon;
using Core.Domain.Repositories.Abstactions;
using Core.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Services
{
    internal sealed class PokemonService : ServiceBase, IPokemonService
    {
        public PokemonService(IRepositoryManager repositoryManager) : base(repositoryManager)
        {
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

        public async Task<List<PokemonDto>> GetPokemonAsync(CancellationToken cancellationToken = default)
        {
            var result = await _repositoryManager.PokemonRepository.GetPokemonAsync(cancellationToken);

            var dtos = result.Select(p => new PokemonDto
            {
                ID = p.ID,
                Name = p.Name,
                Types = p.Types,
                Sprites = p.Sprites,
                Height = p.Height,
                Weight = p.Weight,
                Cry = p.Cry,
                Stats = p.Stats,
                EvolutionReqs = p.EvolutionReqs
            }
            ).ToList();
            return dtos;
        }
    }
}
