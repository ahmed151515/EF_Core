using L17_Raw_SQL_Query.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace L17_Raw_SQL_Query.Data.Config
{
	public class CourseOverviewConfiguration : IEntityTypeConfiguration<CourseOverview>
	{
		public void Configure(EntityTypeBuilder<CourseOverview> builder)
		{
			builder.HasNoKey();
			builder.ToView("CourseOverview");
		}
	}
}

