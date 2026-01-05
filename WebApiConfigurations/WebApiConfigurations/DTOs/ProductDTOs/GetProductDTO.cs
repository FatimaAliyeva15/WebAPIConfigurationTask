namespace WebApiConfigurations.DTOs.ProductDTOs
{
    public class GetProductDTO
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }
        public string Currency { get; set; }
        public string CategoryName { get; set; }
    }
}
