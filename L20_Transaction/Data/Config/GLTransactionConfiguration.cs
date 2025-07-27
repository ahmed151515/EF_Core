using L20_Transaction.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace L20_Transaction.Data.Config
{
	public class GLTransactionConfiguration : IEntityTypeConfiguration<GLTransaction>
	{
		public void Configure(EntityTypeBuilder<GLTransaction> builder)
		{
			builder.HasKey(x => x.Id);

			builder.Property(x => x.Notes)
				.HasColumnType("VARCHAR")
				.HasMaxLength(255).IsRequired();

			builder.Property(x => x.Amount).HasColumnType("decimal(18, 2)").IsRequired();

			builder.Property(p => p.CreatedAt).IsRequired();

			builder.ToTable("GLTransactions");
		}
	}
}
