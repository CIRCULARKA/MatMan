using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MatMan.Domain.Models;
using MatMan.Data.Attributes;

namespace MatMan.Data.Configurations
{
	[ModelConfiguration]
	public class UnitConfiguration : IEntityTypeConfiguration<Unit>
	{
		public void Configure(EntityTypeBuilder<Unit> builder)
		{
			builder.HasKey(u => u.ID);
			builder.Property(u => u.FullName).
				IsRequired();
			builder.HasIndex(u => u.FullName).
				IsUnique();
			builder.Property(u => u.ShortName).IsRequired();
			// builder.HasData(
			// 	new Unit { ID = Guid.NewGuid(), ShortName = "м", FullName = "метр", AttitudeToMeter = 1 },
			// 	new Unit { ID = Guid.NewGuid(), ShortName = "см", FullName = "сантиметр", AttitudeToMeter = 0.01 },
			// 	new Unit { ID = Guid.NewGuid(), ShortName = "мм", FullName = "миллиметр", AttitudeToMeter = 0.001 }
			// );
		}
	}
}
