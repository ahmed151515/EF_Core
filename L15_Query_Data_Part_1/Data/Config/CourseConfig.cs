using L15_Query_Data_Part_1.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace L15_Query_Data_Part_1.Data.Config
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

			builder.ToTable("Courses");

		}



	}


}

