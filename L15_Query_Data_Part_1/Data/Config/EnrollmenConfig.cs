using L15_Query_Data_Part_1.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace L15_Query_Data_Part_1.Data.Config
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
