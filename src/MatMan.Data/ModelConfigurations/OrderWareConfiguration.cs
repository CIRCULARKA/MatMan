using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MatMan.Domain.Models;
using MatMan.Data.Attributes;

namespace MatMan.Data.Configurations
{
	[ModelConfiguration]
	public class OrderWareConfiguration : IEntityTypeConfiguration<OrderComponent<Ware>>
	{
		public void Configure(EntityTypeBuilder<OrderComponent<Ware>> builder)
		{
			builder.HasKey(ow => ow.ID);
			builder.Property(p => p.OrderID).IsRequired();
			builder.Property(p => p.ComponentID).IsRequired();
			builder.Property(p => p.ComponentAmount).IsRequired();
		}
	}
}
