using WebApiConfigurations.Entities.Common;

namespace WebApiConfigurations.Entities
{
    public class Product: BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; } 
        public int Count { get; set; }
        public string Currency {  get; set; }
        public Guid? CategoryId { get; set; }
        public Category Category { get; set; }
        public List<OrderItem> OrderItems { get; set; }

    }
}
