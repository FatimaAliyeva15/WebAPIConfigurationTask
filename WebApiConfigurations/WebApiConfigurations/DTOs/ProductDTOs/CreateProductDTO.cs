namespace WebApiConfigurations.DTOs.ProductDTOs
{
    public class CreateProductDTO
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }
        public string Currency { get; set; }
        public Guid? CategoryId { get; set; }
    }
}
