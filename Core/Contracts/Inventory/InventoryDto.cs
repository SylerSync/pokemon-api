namespace Core.Contracts.Inventory
{
    public class InventoryDto
    {
        public required string UserEmail { get; set; }
        public required List<InventorySlotDto> Items{  get; set; }
        public required int Funds { get; set; }
    }

    public class InventorySlotDto
    {
        public required string ItemId { get; set; }
        public required int Count { get; set; }
    }
}
