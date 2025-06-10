using L11_Migration_Part_02_Relationships.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace L11_Migration_Part_02_Relationships.Data.Config
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

			builder.HasData(LoadInstructors());

			builder.ToTable("Instructors");
		}

		private Instructor[] LoadInstructors() => new Instructor[]
			{
				 new Instructor { Id = 1, FName = "Ahmed", LName = "Abdullah",OfficeId=1},
				new Instructor { Id = 2, FName = "Yasmeen", LName = "Yasmeen",OfficeId=2},
				new Instructor { Id = 3, FName = "Khalid", LName = "Hassan",OfficeId=3},
				new Instructor { Id = 4, FName = "Nadia", LName = "Ali", OfficeId=4},
				new Instructor { Id = 5, FName = "Ahmed", LName = "Abdullah",OfficeId=5},
			};

	}
}
