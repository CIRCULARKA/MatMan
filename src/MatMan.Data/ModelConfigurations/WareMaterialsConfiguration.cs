using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MatMan.Domain.Models;
using MatMan.Data.Attributes;

namespace MatMan.Data.Configurations
{
	public class WareMaterialConfiguration : IEntityTypeConfiguration<WareMaterial>
	{
		[ModelConfiguration]
		public void Configure(EntityTypeBuilder<WareMaterial> builder)
		{
			builder.HasKey(wm => wm.ID);
			builder.Property(wm => wm.MaterialID).IsRequired();
			builder.Property(wm => wm.WareID).IsRequired();
			builder.Property(wm => wm.MaterialsAmount).IsRequired();
		}
	}
}
