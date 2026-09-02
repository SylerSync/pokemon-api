using Core.Contracts.PokeBox;
using Core.Contracts.Pokemon;
using Core.Domain.DataObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.Abstractions
{
    public interface IPokeBoxService
    {
        Task<PokeBoxDto> AddToUsersPokeBox(string userID, PokemonFullInfoDto pokemon, CancellationToken cancellationToken = default);
        Task<PokeBoxDto> GetPokeBox(string userID, CancellationToken cancellationToken = default);
    }
}
