using Core.Domain.DataObjects;
using Core.Domain.Repositories.Abstactions;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using SharpCompress.Compressors.ZStandard;

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

        public async Task<bool> RemoveFromUsersPokeBox(string userID, CaughtPokemon pokemon, CancellationToken cancellationToken = default)
        {
            try
            {
                var dbPokemon = await _dbContext.CaughtPokemon
                    .FirstOrDefaultAsync(p => p.UserEmail == userID && p._id == pokemon._id, cancellationToken);

                if (dbPokemon == null) return false;

                _dbContext.CaughtPokemon.Remove(dbPokemon);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<CaughtPokemon> UpdateCaughtPokemon(string userID, CaughtPokemon pokemon, CancellationToken cancellationToken = default)
        {
            try
            {
                var dbPokemon = await _dbContext.CaughtPokemon
                    .FirstOrDefaultAsync(p => p.UserEmail == userID && p._id == pokemon._id, cancellationToken);

                if (dbPokemon == null) throw new InvalidOperationException("Pokemon not found");

                dbPokemon.TotalHP = pokemon.TotalHP;
                dbPokemon.CurrentHP = pokemon.CurrentHP;
                dbPokemon.TotalFaints = pokemon.TotalFaints;
                dbPokemon.TotalKOs = pokemon.TotalKOs;
                dbPokemon.CurrentExp = pokemon.CurrentExp;
                dbPokemon.Level = pokemon.Level;
                dbPokemon.Stats = pokemon.Stats;
                dbPokemon.Moves = pokemon.Moves;

                await _dbContext.SaveChangesAsync(cancellationToken);

                return dbPokemon;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error updating Pokemon: " + ex.Message, ex);
            }
        }
    }
}
