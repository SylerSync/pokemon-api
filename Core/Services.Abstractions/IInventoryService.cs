using Core.Contracts.Inventory;

namespace Core.Services.Abstractions
{
    public interface IInventoryService
    {
        public Task<InventoryDto?> GetInventoryById(string email);
        public Task<InventoryDto?> BuyItem(string email, string itemId, int quantity = 1);
        public Task<InventoryDto?> UseItem(string email, string itemId, int quantity = 1);
        public Task<InventoryDto?> AddItem(string email, string itemId, int quantity = 1);
        public Task<InventoryDto?> AddFunds(string email, int amount = 0);

    }
}
