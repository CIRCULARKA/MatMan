using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MatMan.Domain.Models;
using MatMan.Data.Attributes;

namespace MatMan.Data.Configurations
{
	[ModelConfiguration]
	public class WorkRuleConfiguration : IEntityTypeConfiguration<WorkRule>
	{
		public void Configure(EntityTypeBuilder<WorkRule> builder)
		{
			builder.Property(wr => wr.WorkID);
			builder.Property(wr => wr.AdditionalPerimeter);
		}
	}
}
