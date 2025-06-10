using L11_Migration_Part_02_Relationships.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace L11_Migration_Part_02_Relationships.Data.Config
{
	public class EnrollmenConfig : IEntityTypeConfiguration<Enrollment>
	{
		public void Configure(EntityTypeBuilder<Enrollment> builder)
		{
			builder.HasKey(e => new { e.SectionId, e.StudentId });






			builder.HasData(LoadEnrollments());
			builder.ToTable("Enrollmens");

		}
		private static List<Enrollment> LoadEnrollments() => new List<Enrollment>
		{

			new Enrollment() { StudentId = 1, SectionId = 6 },
			new Enrollment() { StudentId = 2, SectionId = 6 },
			new Enrollment() { StudentId = 3, SectionId = 7 },
			new Enrollment() { StudentId = 4, SectionId = 7 },
			new Enrollment() { StudentId = 5, SectionId = 8 },
			new Enrollment() { StudentId = 6, SectionId = 8 },
			new Enrollment() { StudentId = 7, SectionId = 9 },
			new Enrollment() { StudentId = 8, SectionId = 9 },
			new Enrollment() { StudentId = 9, SectionId = 10 },
			new Enrollment() { StudentId = 10, SectionId = 10 }
		};
	}

}
