using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Core.Contracts.Item;
using Core.Services.Abstractions;

namespace controllers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        // Set the service manager for service access
        public ItemController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ItemDto>>> GetAllItems()
        {
            var items = await _serviceManager.ItemService.GetAllItemsAsync();
            return Ok(items);
        }



    }
}
