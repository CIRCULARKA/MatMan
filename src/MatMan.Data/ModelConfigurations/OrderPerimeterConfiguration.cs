using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MatMan.Domain.Models;
using MatMan.Data.Attributes;

namespace MatMan.Data.Configurations
{
	[ModelConfiguration]
	public class OrderPerimeterConfiguration : IEntityTypeConfiguration<OrderPerimeter>
	{
		public void Configure(EntityTypeBuilder<OrderPerimeter> builder)
		{
			builder.Property(op => op.OrderID).IsRequired();
			builder.Property(op => op.PerimeterTypeID).IsRequired();
			builder.Property(op => op.Perimeter).IsRequired();
		}
	}
}
