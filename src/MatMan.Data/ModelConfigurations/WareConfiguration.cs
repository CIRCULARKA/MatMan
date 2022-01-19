using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MatMan.Domain.Models;
using MatMan.Data.Attributes;

namespace MatMan.Data.Configurations
{
	[ModelConfiguration]
	public class WareConfiguration : IEntityTypeConfiguration<Ware>
	{
		public void Configure(EntityTypeBuilder<Ware> builder)
		{
			builder.Property(m => m.Name).IsRequired();
			builder.Property(m => m.Name).HasMaxLength(30);
			builder.HasIndex(m => m.Name).IsUnique();
		}
	}
}
