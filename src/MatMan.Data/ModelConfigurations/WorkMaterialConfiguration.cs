using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MatMan.Domain.Models;
using MatMan.Data.Attributes;

namespace MatMan.Data.Configurations
{
	[ModelConfiguration]
	public class WorkMaterialConfiguration : IEntityTypeConfiguration<WorkMaterial>
	{
		public void Configure(EntityTypeBuilder<WorkMaterial> builder)
		{
			builder.HasKey(wm => wm.ID);
			builder.Property(wm => wm.MaterialID).IsRequired();
			builder.Property(wm => wm.WorkID).IsRequired();
			builder.Property(wm => wm.MaterialsAmount).IsRequired();
		}
	}
}
