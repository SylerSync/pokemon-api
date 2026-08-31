using Core.Contracts.Inventory;
using Core.Domain.DataObjects;
using Core.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace controllers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : BaseController
    {
        public InventoryController(IServiceManager serviceManager) : base(serviceManager)
        {

        }

        [HttpGet("{email}")]
        public async Task<ActionResult> GetInventoryByEmail(string email)
        {
            var inventory = await _serviceManager.InventoryService.GetInventoryById(email);
            if(inventory == null)
            {
                return NotFound($"Could not find inventory for {email}");
            }
            return Ok(inventory);
        }

        [HttpPost("buyItem")]
        public async Task<ActionResult> BuyInventoryItem([FromBody] InventoryRequest request)
        {
            if(request == null) { return BadRequest("No request data sent."); }
            var inventory = await _serviceManager.InventoryService.BuyItem(request.Email, request.ItemId, request.Quantity);
            if (inventory == null) { return BadRequest($"Failed to buy item {request.ItemId} for {request.Email}."); }
            return Ok(inventory);
        }

        [HttpPost("addItem")]
        public async Task<ActionResult> AddInventoryItem([FromBody] InventoryRequest request)
        {
            if(request == null) { return BadRequest("No request data sent"); }

            var inventory = await _serviceManager.InventoryService.AddItem(request.Email, request.ItemId, request.Quantity);
            if(inventory == null) { return BadRequest($"Failed to add item {request.ItemId} to inventory for {request.Email}."); }
            return Ok(inventory);
        }

        [HttpPost("useItem")]
        public async Task<ActionResult> UseInventoryItem([FromBody] InventoryRequest request)
        {
            if (request == null) { return BadRequest("No request data sent."); }
            var inventory = await _serviceManager.InventoryService.UseItem(request.Email, request.ItemId, request.Quantity);
            if (inventory == null) { return BadRequest($"Failed to use item {request.ItemId} for {request.Email}"); }
            return Ok(inventory);
        }

        [HttpPost("addFunds/{email}/{amount}")]
        public async Task<ActionResult> AddFunds(string email, int amount)
        {
            var invDto = await _serviceManager.InventoryService.AddFunds(email, amount);
            if(invDto == null)
            {
                return BadRequest($"Failed to add funds to account {email}");
            }
            return Ok(invDto);
        }

    }
}
