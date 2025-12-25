using WebApiConfigurations.Entities;

namespace WebApiConfigurations.DTOs.OrderItemDTOs
{
    public class GetOrderItemDTO
    {
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
