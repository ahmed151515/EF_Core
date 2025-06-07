using L10_EF_Migration.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace L10_EF_Migration.Data.Config
{
	public class CourseConfig : IEntityTypeConfiguration<Course>
	{
		public void Configure(EntityTypeBuilder<Course> builder)
		{
			builder.HasKey(c => c.Id);
			builder.Property(p => p.Id).ValueGeneratedNever();

			//builder.Property(p => p.CourseName).HasMaxLength(255); // nvarchar
			builder.Property(p => p.CourseName).HasColumnType("varchar").HasMaxLength(255).IsRequired();

			builder.Property(p => p.Price).HasPrecision(15, 2).IsRequired();

			builder.HasData(LoadCourses());
			builder.ToTable("Courses");

		}
		private static List<Course> LoadCourses() => new List<Course> {
				new Course { Id = 1, CourseName = "Mathematics", Price = 1000m },
				new Course { Id = 2, CourseName ="Physics", Price =2000m},
				new Course { Id = 3, CourseName ="Chemistry", Price =1500m},
				new Course { Id = 4, CourseName ="Biology", Price =1200m},
				new Course { Id = 5, CourseName ="Computer Science", Price =3000m}
		};


	}
}
