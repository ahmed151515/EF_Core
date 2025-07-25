using L18_Save_Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace L18_Save_Data.Data.Config
{
	public partial class BookConfig : IEntityTypeConfiguration<Book>
	{
		public void Configure(EntityTypeBuilder<Book> builder)
		{
			builder.HasKey(b => b.Id);
			builder.Property(x => x.Id).ValueGeneratedNever();

			builder.Property(x => x.Title)
				.HasColumnType("VARCHAR")
				.HasMaxLength(255).IsRequired();

			builder.Property(x => x.Price)
			   .HasColumnType("decimal(18, 2)").IsRequired();

			builder.HasOne(x => x.Author)
				.WithMany(x => x.Books)
				.HasForeignKey(x => x.AuthorId)
				.IsRequired();

			builder.ToTable("Books");
		}
	}
}
