using OrderSystem.Core.Enums;

namespace OrderSystem.DTO
{
    public class CreateOrderDTO
    {
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public List<CreateOrderItemDto> OrderItems { get; set; }
        public string PaymentMethod { get; set; }
        public string Status { get; set; }
    }
}
