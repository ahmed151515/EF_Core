using L10_EF_Migration.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace L10_EF_Migration.Data.Config
{
	public class InstructorConfig : IEntityTypeConfiguration<Instructor>
	{
		public void Configure(EntityTypeBuilder<Instructor> builder)
		{
			builder.HasKey(c => c.Id);
			builder.Property(p => p.Id).ValueGeneratedNever();

			builder.Property(p => p.FName).HasColumnType("varchar").HasMaxLength(50).IsRequired();
			builder.Property(p => p.LName).HasColumnType("varchar").HasMaxLength(50).IsRequired();

			builder.HasData(LoadInstructors());

			builder.ToTable("Instructors");
		}

		private Instructor[] LoadInstructors() => new Instructor[]
			{
				 new Instructor { Id = 1, FName = "Ahmed", LName = "Abdullah"},
				new Instructor { Id = 2, FName = "Yasmeen", LName = "Yasmeen"},
				new Instructor { Id = 3, FName = "Khalid", LName = "Hassan"},
				new Instructor { Id = 4, FName = "Nadia", LName = "Ali"},
				new Instructor { Id = 5, FName = "Ahmed", LName = "Abdullah"},
			};
	}
}
