using L12_EF_Migration_Part_03.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace L12_EF_Migration_Part_03.Data.Config
{
	public class StudentConfig : IEntityTypeConfiguration<Student>
	{
		public void Configure(EntityTypeBuilder<Student> builder)
		{
			builder.HasKey(c => c.Id);
			builder.Property(p => p.Id).ValueGeneratedNever();

			builder.Property(p => p.FName).HasColumnType("varchar").HasMaxLength(50).IsRequired();
			builder.Property(p => p.LName).HasColumnType("varchar").HasMaxLength(50).IsRequired();




			builder.ToTable("Students");
		}



	}
}
