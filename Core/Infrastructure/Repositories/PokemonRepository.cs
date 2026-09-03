using Core.Domain.DataObjects;
using Core.Domain.Enums;
using Core.Domain.Repositories.Abstactions;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Infrastructure.Repositories
{
    internal sealed class PokemonRepository : RepositoryBase, IPokemonRepository
    {
        public PokemonRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public Task AddPokemonAsync(Pokemon pokemon, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task DeletePokemonAsync(Pokemon pokemon, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Pokemon> EditPokemonAsync(Pokemon oldPokemon, Pokemon newPokemon, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Pokemon> EvolvePokemonAsync(Pokemon pokemon, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Pokemon>> GetPokemonAsync(CancellationToken cancellationToken = default)
        {
            var pokemon = await _dbContext.Pokemon.ToListAsync(cancellationToken);
            return pokemon;
        }

        public async Task<Pokemon> GetPokemonByIDAsync(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var pokemon = await _dbContext.Pokemon.FirstOrDefaultAsync(p => p.ID == id, cancellationToken);
                if (pokemon == null)
                {
                    throw new Exception($"Pokemon with ID {id} not found.");
                }
                return pokemon;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving the Pokemon with ID {id}: {ex.Message}");
            }
        }
    }
}
