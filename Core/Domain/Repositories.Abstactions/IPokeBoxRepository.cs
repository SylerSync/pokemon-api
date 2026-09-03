using Core.Domain.DataObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Repositories.Abstactions
{
    public interface IPokeBoxRepository
    {
        Task<bool> AddToUsersPokeBox(string userID, CaughtPokemon pokemon, CancellationToken cancellationToken = default);
        Task<PokeBox> GetUsersPokeBox(string userID, CancellationToken cancellationToken = default);
        Task<bool> RemoveFromUsersPokeBox(string userID, CaughtPokemon pokemon, CancellationToken cancellationToken = default);
        Task<CaughtPokemon> UpdateCaughtPokemon(string userID, CaughtPokemon pokemon, CancellationToken cancellationToken = default);
    }
}
