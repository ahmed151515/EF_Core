using L11_Migration_Part_02_Relationships.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace L11_Migration_Part_02_Relationships.Data.Config
{
	public class StudentConfig : IEntityTypeConfiguration<Student>
	{
		public void Configure(EntityTypeBuilder<Student> builder)
		{
			builder.HasKey(c => c.Id);
			builder.Property(p => p.Id).ValueGeneratedNever();

			builder.Property(p => p.FName).HasColumnType("varchar").HasMaxLength(50).IsRequired();
			builder.Property(p => p.LName).HasColumnType("varchar").HasMaxLength(50).IsRequired();



			builder.HasData(LoadStudents());

			builder.ToTable("Students");
		}

		private Student[] LoadStudents() => new Student[]
			{
				new Student() { Id = 1, FName = "Fatima", LName = "Ali" },
				new Student() { Id = 2, FName = "Noor" , LName = "Saleh" },
				new Student() { Id = 3, FName = "Omar" , LName = "Youssef" },
				new Student() { Id = 4, FName = "Huda" , LName = "Ahmed" },
				new Student() { Id = 5, FName = "Amira" , LName = "Tariq" },
				new Student() { Id = 6, FName = "Zainab" , LName = "Ismail" },
				new Student() { Id = 7, FName = "Yousef" , LName = "Farid" },
				new Student() { Id = 8, FName = "Layla" , LName = "Mustafa" },
				new Student() { Id = 9, FName = "Mohammed" , LName = "Adel" },
				new Student() { Id = 10, FName = "Samira" , LName = "Nabil" }
			};

	}
}
