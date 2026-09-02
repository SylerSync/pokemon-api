using Core.Domain.DataObjects;
using Core.Domain.Repositories.Abstactions;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace Core.Infrastructure.Repositories
{
    internal sealed class PokeBoxRepository : RepositoryBase, IPokeBoxRepository
    {
        public PokeBoxRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> AddToUsersPokeBox(string userID, CaughtPokemon pokemon, CancellationToken cancellationToken = default)
        {
            try
            {

                await _dbContext.CaughtPokemon.AddAsync(pokemon);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<PokeBox> GetUsersPokeBox(string userID, CancellationToken cancellationToken = default)
        {
            List<CaughtPokemon> pokemon = await _dbContext.CaughtPokemon
                .AsNoTracking()
                .Where(p => p.UserEmail == userID)
                .OrderBy(p => p.ID)
                .ToListAsync(cancellationToken);
            var box = new PokeBox
            {
                UserID = userID,
                pokemon = new List<CaughtPokemon>()
            };
            if (pokemon == null) { return box; }
            box.pokemon = pokemon;
            return box;
        }
    }
}
