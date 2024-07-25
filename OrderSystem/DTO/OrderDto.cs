using OrderSystem.Core.Enums;

namespace OrderSystem.DTO
{
    public class OrderDto
    {
        
        public string CustomerName { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public string PaymentMethod { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public List<OrderItemDto> OrderItems { get; set; }


    }
}
