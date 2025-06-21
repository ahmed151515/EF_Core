using L15_Query_Data_Part_1.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace L15_Query_Data_Part_1.Data.Config
{
	public class InstructorConfig : IEntityTypeConfiguration<Instructor>
	{
		public void Configure(EntityTypeBuilder<Instructor> builder)
		{
			builder.HasKey(c => c.Id);
			builder.Property(p => p.Id).ValueGeneratedNever();

			builder.Property(p => p.FName).HasColumnType("varchar").HasMaxLength(50).IsRequired();
			builder.Property(p => p.LName).HasColumnType("varchar").HasMaxLength(50).IsRequired();

			builder.HasOne(instructor => instructor.Office)
				.WithOne(office => office.Instructor)
				.HasForeignKey<Instructor>(instructor => instructor.OfficeId)
				.IsRequired(false);

			builder.HasIndex(instructor => instructor.OfficeId).IsUnique();


			builder.ToTable("Instructors");

		}



	}
}
