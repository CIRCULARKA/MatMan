using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MatMan.Domain.Models;
using MatMan.Data.Attributes;

namespace MatMan.Data.Configurations
{
	[ModelConfiguration]
	public class WorkConfiguration : IEntityTypeConfiguration<Work>
	{
		public void Configure(EntityTypeBuilder<Work> builder)
		{
			builder.HasKey(w => w.ID);
			builder.Property(w => w.Name).
				IsRequired().HasMaxLength(30);
			builder.HasIndex(w => w.Name).IsUnique();
			builder.Property(w => w.IsApplicableToWholePerimeter).IsRequired();
		}
	}
}
