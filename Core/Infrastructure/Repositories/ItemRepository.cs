using Core.Domain.DataObjects;
using Core.Domain.Repositories.Abstactions;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Core.Infrastructure.Repositories
{
    internal sealed class ItemRepository : RepositoryBase, IItemRepository
    {
        public ItemRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IReadOnlyList<BaseItem>> GetAllAsync()
        { 
            return await _dbContext.Items.ToListAsync();
        }

        public async Task<IReadOnlyList<BaseItem>> GetByCategoryAsync(string category)
        {
            return await _dbContext.Items
            .Where(x => x.Category.ToLower() == category.ToLower())
            .ToListAsync();
        }

        public Task<BaseItem> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
