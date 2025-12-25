using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApiConfigurations.Entities;

namespace WebApiConfigurations.DAL.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(x => x.OrderDate).IsRequired().HasColumnType("datetime2").HasDefaultValueSql("GETDATE()");
            builder.Property(x => x.TotalAmount).IsRequired().HasColumnType("decimal(18,2)");
        }
    }
}
