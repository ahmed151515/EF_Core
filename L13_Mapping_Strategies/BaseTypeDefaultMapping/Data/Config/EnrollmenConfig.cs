using L13_Mapping_Strategies.BaseTypeDefaultMapping.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace L13_Mapping_Strategies.BaseTypeDefaultMapping.Data.Config
{
	public class EnrollmenConfig : IEntityTypeConfiguration<Enrollment>
	{
		public void Configure(EntityTypeBuilder<Enrollment> builder)
		{
			builder.HasKey(e => new { e.SectionId, e.StudentId });






			builder.ToTable("Enrollmens");

		}

	}

}
