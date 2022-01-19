using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MatMan.Domain.Models;
using MatMan.Data.Attributes;

namespace MatMan.Data.Configurations
{
	[ModelConfiguration]
	public class OrderConfiguration : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			builder.HasKey(o => o.ID);
			builder.Property(o => o.Name).
				IsRequired().
					HasMaxLength(40);
			builder.HasIndex(o => o.Name).IsUnique();
			builder.Property(o => o.Desription).HasMaxLength(300);
		}
	}
}
