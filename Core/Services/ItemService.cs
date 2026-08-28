using Core.Contracts.Item;
using Core.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Domain.DataObjects;
using Core.Domain.Repositories;
using Core.Domain.Repositories.Abstactions;
using System.Linq;
using System.Diagnostics.Contracts;
using Core.Infrastructure.Repositories;

namespace Core.Services
{
    internal sealed class ItemService : ServiceBase, IItemService
    {

        public ItemService(IRepositoryManager repositoryManager): base(repositoryManager)
        {
            
        }

        //Get a single item using and Id
        public async Task<ItemDto> GetItemByIdAsync(string Id)
        {
            BaseItem item = await _repositoryManager.ItemRepository.GetByIdAsync(Id);
            return item == null ? null : MapToDto(item);
        }

        //Get all items
        public async Task<IReadOnlyList<ItemDto>> GetAllItemsAsync()
        {
            IReadOnlyList<BaseItem> items = await _repositoryManager.ItemRepository.GetAllAsync();
            return items.Select(MapToDto).ToList().AsReadOnly();
        }

        //Get all items based on a category
        public async Task<IReadOnlyList<ItemDto>> GetItemsByCategoryAsync(string category)
        {
            IReadOnlyList<BaseItem> items = await _repositoryManager.ItemRepository.GetByCategoryAsync(category);
            return items.Select(MapToDto).ToList().AsReadOnly();
        }

        // DTO mapping
        private ItemDto MapToDto(BaseItem item)
        {
            var dto = new ItemDto
            {
                Id = item.Id,
                Name = item.Name,
                Category = item.Category,
                Cost = item.Cost
            };

            switch (item)
            {
                case RecoveryItem recovery:
                    MapEffectDetails(recovery.Effect, dto);
                    break;
                case PokeballItem pokeball:
                    dto.CatchPower = pokeball.CatchPower;
                    dto.Description = pokeball.Description;
                    break;
                case TechnicalMachineItem technicalMachine:
                    dto.Move = technicalMachine.Move;
                    dto.MoveName = technicalMachine.MoveName;
                    dto.Type = technicalMachine.Type;
                    break;
                case MegaEvolutionItem megaEvolution:
                    dto.PokemonName = megaEvolution.PokemonName;
                    dto.MegaFormName = megaEvolution.MegaFormName;
                    dto.Description = megaEvolution.Description;
                    break;
                case EvolutionItem evolution:
                    dto.Description = evolution.Description;
                    break;
            }
            return dto;
        }

        // Map the effects of Recovery items to the dto
        private void MapEffectDetails(ItemEffect effect, ItemDto dto)
        {
            if (effect == null) return;

            dto.EffectType = effect.EffectType;

            switch (effect)
            {
                case HealEffect heal:
                    dto.Amount = heal.Amount;
                    break;
                case StatusHealEffect statusHeal:
                    dto.Status = statusHeal.Status;
                    break;
                case PpHealEffect ppHeal:
                    dto.Scope = ppHeal.Scope;
                    dto.Amount = ppHeal.Amount;
                    break;
                case PpMaxRaise ppMax:
                    dto.Scope = ppMax.Scope;
                    dto.Stages = ppMax.Stages;
                    break;
            }

        }

    }
}
