using L12_EF_Migration_Part_03.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace L12_EF_Migration_Part_03.Data.Config
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
