namespace Shared.DataTransferObject.OrderDTOS
{
    public class OrderItemDto
    {
        public string ProductName { get; set; } = default!;
        public string PictureUre { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}