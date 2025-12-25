namespace WebApiConfigurations.DTOs.OrderDTOs
{
    public class CreateOrderDTO
    {
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
