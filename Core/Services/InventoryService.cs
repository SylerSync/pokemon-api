using Core.Contracts.Inventory;
using Core.Domain.DataObjects;
using Core.Domain.Repositories.Abstactions;
using Core.Services.Abstractions;

namespace Core.Services
{
    internal sealed class InventoryService : ServiceBase, IInventoryService
    {
        public InventoryService(IRepositoryManager repositoryManager) : base(repositoryManager)
        {

        }
        // Add item bypassing fund check, useful for returning held items
        public async Task<InventoryDto?> AddItem(string email, string itemId, int quantity = 1)
        {
            if (quantity <= 0) return null;
            // Collect current inventory
            var inventory = await GetOrCreateInventoryAsync(email);

            InventorySlot? existingSlot = inventory.Items.FirstOrDefault(slot => slot.ItemId == itemId);
            if(existingSlot == null)
            {
                inventory.Items.Add(new InventorySlot
                {
                    ItemId = itemId,
                    Count = quantity
                });
            }
            else
            {
                existingSlot.Count += quantity;
            }

            //Update inventory
            if(await _repositoryManager.InventoryRepository.UpdateInventoryByEmail(inventory))
            {
                return MapToDto(inventory);
            }
            else
            { return null; }
        }

        // Add item with fund checking, useful for shop purchases
        public async Task<InventoryDto?> BuyItem(string email, string itemId, int quantity = 1)
        {
            if (quantity <= 0) return null;
            var inventory = await GetOrCreateInventoryAsync(email);
            var item = await _repositoryManager.ItemRepository.GetByIdAsync(itemId);
            if (item == null) return null;

            // Check if the user has funds
            if(inventory.Funds < (item.Cost * quantity))
            {
                return null;
            }

            inventory.Funds -= (item.Cost * quantity);

            InventorySlot? existingSlot = inventory.Items.FirstOrDefault(slot => slot.ItemId == itemId);
            if(existingSlot == null)
            {
                inventory.Items.Add(new InventorySlot
                {
                    ItemId = itemId,
                    Count = quantity
                });
            }
            else
            {
                existingSlot.Count += quantity;   
            }
            if(await _repositoryManager.InventoryRepository.UpdateInventoryByEmail(inventory))
            {
                return MapToDto(inventory);
            }
            else { return null; }

        }

        // Get a user inventory by email
        public async Task<InventoryDto?> GetInventoryById(string email)
        {
            var inventory = await GetOrCreateInventoryAsync(email);
            return MapToDto(inventory);
        }

        // Remove an item from user inventory if count allows
        public async Task<InventoryDto?> UseItem(string email, string itemId, int quantity = 1)
        {
            if (quantity <= 0) return null;
            var inventory = await GetOrCreateInventoryAsync(email);

            InventorySlot? existingSlot = inventory.Items.FirstOrDefault(slot => slot.ItemId == itemId);
            if (existingSlot == null) return null;
            if(existingSlot.Count > 0 && existingSlot.Count >= quantity)
            {
                existingSlot.Count -= quantity;
            }
            else
            { return null; }

            if (await _repositoryManager.InventoryRepository.UpdateInventoryByEmail(inventory))
            {
                return MapToDto(inventory);
            }
            else { return null; }

        }

        // Add funds to a user's inventory.
        public async Task<InventoryDto?> AddFunds(string email, int amount = 0)
        {
            var inventory = await GetOrCreateInventoryAsync(email);

            inventory.Funds += amount;

            if (await _repositoryManager.InventoryRepository.UpdateInventoryByEmail(inventory)) { return MapToDto(inventory); }

            return null;
        }

        // Helper method to build a Dto from Domain Entity
        public InventoryDto? MapToDto(Inventory inventory)
        {
            if (inventory == null) return null;
            List<InventorySlot> domainSlots = inventory.Items;
            List<InventorySlotDto> dtoSlots = new List<InventorySlotDto>();

            foreach(var slot in domainSlots)
            {
                dtoSlots.Add(new InventorySlotDto
                {
                    ItemId = slot.ItemId,
                    Count = slot.Count
                });
            }

            var invDto = new InventoryDto
            {
                UserEmail = inventory.UserEmail,
                Funds = inventory.Funds,
                Items = dtoSlots
            };

            return invDto;

        }

        // Helper method to get or create a new inventory object
        private async Task<Inventory> GetOrCreateInventoryAsync(string email)
        {
            var inventory = await _repositoryManager.InventoryRepository.GetInventoryByEmail(email);

            if (inventory == null)
            {
                inventory = new Inventory
                {
                    UserEmail = email,
                    Funds = 0,
                    Items = new List<InventorySlot>()
                };
            }

            return inventory;
        }


    }
}
