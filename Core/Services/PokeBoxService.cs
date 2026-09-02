using Core.Contracts.PokeBox;
using Core.Contracts.Pokemon;
using Core.Contracts.User;
using Core.Domain.DataObjects;
using Core.Domain.Enums;
using Core.Domain.Repositories.Abstactions;
using Core.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services
{
    internal sealed class PokeBoxService : ServiceBase, IPokeBoxService
    {
        public PokeBoxService(IRepositoryManager repositoryManager) : base(repositoryManager)
        {
        }

        public async Task<PokeBoxDto> AddToUsersPokeBox(string userID, PokemonFullInfoDto pokemon, CancellationToken cancellationToken = default)
        {
            try
            {
                var newPokemon = new CaughtPokemon
                {
                    _id = pokemon._id,
                    UserEmail = userID,
                    ID = pokemon.ID,
                    Name = pokemon.Name,
                    Shiny = pokemon.Shiny,
                    Types = pokemon.Types,
                    Sprites = pokemon.Sprites,
                    Height = pokemon.Height,
                    Weight = pokemon.Weight,
                    Cry = pokemon.Cry,
                    CaptureRate = pokemon.CaptureRate,
                    TotalHP = pokemon.TotalHP,
                    CurrentHP = pokemon.CurrentHP,
                    Stats = pokemon.Stats,
                    Moves = pokemon.Moves,
                    LearnableMoves = pokemon.LearnableMoves,
                    TotalKOs = pokemon.TotalKOs,
                    TotalFaints = pokemon.TotalFaints,
                    Level = pokemon.Level,
                    EvolutionReqs = pokemon.EvolutionReqs,
                    BaseExp = pokemon.BaseExp,
                    CurrentExp = pokemon.CurrentExp
                };

                if (!await _repositoryManager.PokeBoxRepository.AddToUsersPokeBox(userID, newPokemon, cancellationToken))
                {
                    throw new Exception("Failed to add Pokemon to user's PokeBox");
                }
                return MapToDto(await _repositoryManager.PokeBoxRepository.GetUsersPokeBox(userID, cancellationToken));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding Pokemon to user's PokeBox: {ex.Message}", ex);
            }
        }

        public async Task<PokeBoxDto> GetPokeBox(string userID, CancellationToken cancellationToken = default)
        {
            try
            {
                return MapToDto(await _repositoryManager.PokeBoxRepository.GetUsersPokeBox(userID, cancellationToken));
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving user's PokeBox: " + ex.Message, ex);
            }
        }

        // DTO Mapping
        public PokeBoxDto MapToDto(PokeBox pokeBox)
        {
            var dto = new PokeBoxDto
            {
                pokemon = pokeBox.pokemon?.Select(p => new PokemonInBoxDto
                {
                    _id = p._id,
                    ID = p.ID,
                    Name = p.Name,
                    Shiny = p.Shiny,
                    Sprites = p.Sprites,
                    Types = p.Types.Select(t => t.ToString()).ToList(),
                    Height = p.Height,
                    Weight = p.Weight,
                    Cry = p.Cry,
                    CaptureRate = p.CaptureRate,
                    TotalHP = p.TotalHP,
                    CurrentHP = p.CurrentHP,
                    Stats = p.Stats.Select(s => new StatDto { Name = s.Name.ToString(), BaseStat = s.BaseStat }).ToList(),
                    Moves = p.Moves,
                    LearnableMoves = p.LearnableMoves,
                    TotalKOs = p.TotalKOs,
                    TotalFaints = p.TotalFaints,
                    Level = p.Level,
                    EvolutionReqs = p.EvolutionReqs,
                    BaseExp = p.BaseExp,
                    CurrentExp = p.CurrentExp

                }).ToList()
            };

            return dto;
        }
    }
}