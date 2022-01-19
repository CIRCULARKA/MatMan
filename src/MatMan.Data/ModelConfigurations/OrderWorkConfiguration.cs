using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MatMan.Domain.Models;
using MatMan.Data.Attributes;

namespace MatMan.Data.Configurations
{
	[ModelConfiguration]
	public class OrderWorkConfiguration : IEntityTypeConfiguration<OrderWork>
	{
		public void Configure(EntityTypeBuilder<OrderWork> builder)
		{
			builder.HasKey(p => p.ID);
			builder.Property(p => p.OrderID).IsRequired();
			builder.Property(p => p.WorkID).IsRequired();
			builder.Property(p => p.Perimeter).IsRequired();
		}
	}
}
