using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Core.Contracts.Item;
using Core.Services.Abstractions;

namespace controllers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : BaseController
    {

        // Set the service manager for service access
        public ItemController(IServiceManager serviceManager): base(serviceManager)
        {
        }

        //GET /api/item
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ItemDto>>> GetAllItems()
        {
            var items = await _serviceManager.ItemService.GetAllItemsAsync();
            return Ok(items);
        }

        //GET /api/item/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetItemByID(string id)
        {
            var item = await _serviceManager.ItemService.GetItemByIdAsync(id);
            if (item == null)
            {
                return NotFound($"Item with ID {id} was not found.");
            }

            return Ok(item);
        }

        //GET /api/item/category/{category name}
        [HttpGet("category/{categoryName}")]
        public async Task<ActionResult<IReadOnlyList<ItemDto>>> GetItemsByCategory(string categoryName)
        {
            var items = await _serviceManager.ItemService.GetItemsByCategoryAsync(categoryName);
            return Ok(items);
        }


    }
}
