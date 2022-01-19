using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MatMan.Data.Attributes;

namespace MatMan.Data.Configurations
{
	[ModelConfiguration]
	public class MaterialConfigurationConfiguration : IEntityTypeConfiguration<MatMan.Domain.Models.MaterialConfiguration>
	{
		public void Configure(EntityTypeBuilder<MatMan.Domain.Models.MaterialConfiguration> builder)
		{
			builder.HasKey(mc => mc.ID);
			builder.Property(mc => mc.UnitID).IsRequired();
		}
	}
}
