using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MatMan.Domain.Models;
using MatMan.Data.Attributes;

namespace MatMan.Data.Configurations
{
	[ModelConfiguration]
	public class WorkWareConfiguration : IEntityTypeConfiguration<WorkWare>
	{
		public void Configure(EntityTypeBuilder<WorkWare> builder)
		{
			builder.HasKey(w => w.ID);
			builder.Property(w => w.WareID).IsRequired();
			builder.Property(w => w.WorkID).IsRequired();
		}
	}
}
