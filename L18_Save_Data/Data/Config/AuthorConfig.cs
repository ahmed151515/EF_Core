using L18_Save_Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace L18_Save_Data.Data.Config
{
	public class AuthorConfig : IEntityTypeConfiguration<Author>
	{
		public void Configure(EntityTypeBuilder<Author> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).ValueGeneratedNever();

			builder.Property(x => x.FName)
				.HasColumnType("VARCHAR")
				.HasMaxLength(50).IsRequired();

			builder.Property(x => x.LName)
		   .HasColumnType("VARCHAR")
		   .HasMaxLength(50).IsRequired();

			builder.ToTable("Authors");
		}
	}
}
