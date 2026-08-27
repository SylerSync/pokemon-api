using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Contracts.Item;

namespace Core.Services.Abstractions
{
    public interface IItemService
    {
        Task<ItemDto> GetItemByIdAsync(string Id);
        Task<IReadOnlyList<ItemDto>> GetAllItemsAsync();
        Task<IReadOnlyList<ItemDto>> GetItemsByCategoryAsync(string category);
    }
}
