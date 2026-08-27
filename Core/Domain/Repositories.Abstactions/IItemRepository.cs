using Core.Domain.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Repositories.Abstactions
{
    public interface IItemRepository
    {
        Task<Item> GetByIdAsync(string id);
        Task<IReadOnlyList<Item>> GetAllAsync();
        Task<IReadOnlyList<Item>> GetByCategoryAsync(string category);
    }
}
