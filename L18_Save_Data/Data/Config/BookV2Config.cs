using L18_Save_Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace L18_Save_Data.Data.Config
{
	public partial class BookConfig
	{
		public class BookV2Config : IEntityTypeConfiguration<BookV2>
		{
			public void Configure(EntityTypeBuilder<BookV2> builder)
			{
				builder.HasKey(x => x.Id);
				builder.Property(x => x.Id).ValueGeneratedNever();

				builder.Property(x => x.Title)
					.HasColumnType("VARCHAR")
					.HasMaxLength(255).IsRequired();

				builder.HasOne(x => x.AuthorV2)
					.WithMany(x => x.BookV2s)
					.HasForeignKey(x => x.AuthorV2Id)
					.IsRequired(false)
					.OnDelete(DeleteBehavior.SetNull);
				builder.ToTable("BookV2s");
			}
		}
	}
}
