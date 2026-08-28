using Core.Domain.DataObjects;
using Core.Domain.Repositories.Abstactions;
using Microsoft.EntityFrameworkCore;
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

        public Task<List<Pokemon>> GetPokemonAsync(CancellationToken cancellationToken = default)
        {
            return _dbContext.Pokemon.ToListAsync(cancellationToken);
        }

        public Task<Pokemon> GetPokemonByIDAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
