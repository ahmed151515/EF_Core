using L17_Raw_SQL_Query.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace L17_Raw_SQL_Query.Data.Config
{
	public class EnrollmenConfig : IEntityTypeConfiguration<Enrollment>
	{
		public void Configure(EntityTypeBuilder<Enrollment> builder)
		{
			builder.HasKey(e => new { e.SectionId, e.ParticipantId });






			builder.ToTable("Enrollments");

		}

	}

}
