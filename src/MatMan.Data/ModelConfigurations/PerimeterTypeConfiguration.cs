using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MatMan.Domain.Models;
using MatMan.Data.Attributes;

namespace MatMan.Data.Configurations
{
	[ModelConfiguration]
	public class PerimeterTypeConfiguration : IEntityTypeConfiguration<PerimeterType>
	{
		public void Configure(EntityTypeBuilder<PerimeterType> builder)
		{
			builder.Property(m => m.Name).IsRequired();
			builder.Property(m => m.Name).HasMaxLength(50);
			builder.HasIndex(m => m.Name).IsUnique();
		}
	}
}
