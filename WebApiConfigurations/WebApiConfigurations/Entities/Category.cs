using WebApiConfigurations.Entities.Common;

namespace WebApiConfigurations.Entities
{
    public class Category: BaseEntity
    {
        public string CategoryName { get; set; }
        public string Description { get; set; }

        public List<Product> Products { get; set; }
    }
}
