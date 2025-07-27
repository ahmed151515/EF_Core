using L20_Transaction.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace L20_Transaction.Data.Config
{
	public class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount>
	{
		public void Configure(EntityTypeBuilder<BankAccount> builder)
		{
			builder.HasKey(x => x.AccountId);
			builder.Property(x => x.AccountId).ValueGeneratedNever();

			builder.Property(x => x.AccountHolder)
				.HasColumnType("VARCHAR")
				.HasMaxLength(20).IsRequired();

			builder.Property(x => x.Balance).HasColumnType("decimal(18, 2)").IsRequired();

			builder.HasMany(p => p.GLTransactions);

			builder.ToTable("BankAccounts");
		}
	}
}
