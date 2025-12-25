using WebApiConfigurations.Entities;

namespace WebApiConfigurations.DTOs.OrderItemDTOs
{
    public class UpdateOrderItemDTO
    {
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public Guid? OrderId { get; set; }
        public Guid? ProductId { get; set; }
    }
}
