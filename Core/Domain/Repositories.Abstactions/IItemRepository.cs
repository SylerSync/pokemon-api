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
        Task<BaseItem> GetByIdAsync(string id);
        Task<IReadOnlyList<BaseItem>> GetAllAsync();
        Task<IReadOnlyList<BaseItem>> GetByCategoryAsync(string category);
    }
}
