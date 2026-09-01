namespace Core.Domain.DataObjects
{
    public class Inventory
    {
        public required string UserEmail { get; set; }
        public required List<InventorySlot> Items {  get; set; }
        public required int Funds { get; set; }
    }

    public class InventorySlot
    {
        public required string ItemId {  get; set; }
        public required int Count { get; set; }
    }
}
