namespace OrderSystem.DTO
{
    public class CreateOrderItemDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Dicount { get; set; }
    }
}
