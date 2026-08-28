using Core.Contracts.Item;
using Core.Domain.DataObjects;
using Core.Domain.Repositories.Abstactions;
using Core.Services.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Services
{
    internal sealed class ItemService : ServiceBase, IItemService
    {
        public ItemService(IRepositoryManager repositoryManager) : base(repositoryManager)
        {
        }

        // Get a single item using an Id
        public async Task<ItemDto> GetItemByIdAsync(string Id)
        {
            BaseItem item = await _repositoryManager.ItemRepository.GetByIdAsync(Id);
            return item == null ? null : MapToDto(item);
        }

        // Get all items
        public async Task<IReadOnlyList<ItemDto>> GetAllItemsAsync()
        {
            IReadOnlyList<BaseItem> items = await _repositoryManager.ItemRepository.GetAllAsync();
            return items.Select(MapToDto).ToList().AsReadOnly();
        }

        // Get all items based on a category
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
                    dto.MegaFormName = megaEvolution.MegaName;
                    dto.Description = megaEvolution.Description;
                    break;
                case EvolutionItem evolution:
                    dto.Description = evolution.Description;
                    break;
            }

            return dto;
        }

        // Map the single concrete ItemEffect properties to the DTO
        private void MapEffectDetails(ItemEffect effect, ItemDto dto)
        {
            if (effect == null) return;

            dto.EffectType = effect.EffectType;
            dto.Amount = effect.Amount;
            dto.Percent = effect.Percent;
            dto.Status = effect.Status;
            dto.Scope = effect.Scope;
            dto.Stages = effect.Stages;
        }
    }
}