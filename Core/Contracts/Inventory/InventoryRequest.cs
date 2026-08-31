using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Contracts.Inventory
{
    public class InventoryRequest
    {
        public required string Email { get; set; }
        public required string ItemId { get; set; }
        public int Quantity { get; set; }
    }
}
