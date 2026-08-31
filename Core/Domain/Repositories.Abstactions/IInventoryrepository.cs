using Core.Domain.DataObjects;

namespace Core.Domain.Repositories.Abstactions
{
    public interface IInventoryrepository
    {
        Task<bool> UpdateInventoryByEmail(Inventory inventory);
        Task<Inventory?> GetInventoryByEmail(string email);
    }
}
