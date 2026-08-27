using Core.Domain.DataObjects;
using Core.Domain.Repositories.Abstactions;

namespace Core.Infrastructure.Repositories
{
    public class ItemRepository : IItemRepository
    {
        public Task<IReadOnlyList<BaseItem>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<BaseItem>> GetByCategoryAsync(string category)
        {
            throw new NotImplementedException();
        }

        public Task<BaseItem> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
