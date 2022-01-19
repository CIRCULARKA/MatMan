using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MatMan.Domain.Models;
using MatMan.Data.Attributes;


namespace MatMan.Data.Configurations
{
	[ModelConfiguration]
	public class OrderMaterialsConfiguration : IEntityTypeConfiguration<OrderComponent<Material>>
	{
		public void Configure(EntityTypeBuilder<OrderComponent<Material>> builder)
		{
			builder.HasKey(om => om.ID);
			builder.Property(om => om.OrderID).IsRequired();
			builder.Property(om => om.ComponentID).IsRequired();
			builder.Property(om => om.ComponentAmount).IsRequired();
			builder.HasIndex(om => new { om.OrderID, om.ComponentID }).IsUnique();
		}
	}
}
