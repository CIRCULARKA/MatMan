using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MatMan.Data.Attributes;

namespace MatMan.Data.Configurations
{
	[ModelConfiguration]
	public class WareConfigurationConfiguration : IEntityTypeConfiguration<MatMan.Domain.Models.WareConfiguration>
	{
		public void Configure(EntityTypeBuilder<MatMan.Domain.Models.WareConfiguration> builder)
		{
			builder.HasKey(wc => wc.ID);
			builder.Property(wc => wc.UnitID).IsRequired();
		}
	}
}
