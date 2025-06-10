using L12_EF_Migration_Part_03.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace L12_EF_Migration_Part_03.Data.Config
{
	public class OfficeConfig : IEntityTypeConfiguration<Office>
	{
		public void Configure(EntityTypeBuilder<Office> builder)
		{
			builder.HasKey(c => c.Id);
			builder.Property(p => p.Id).ValueGeneratedNever();

			builder.Property(p => p.OfficeLocation).HasColumnType("varchar").HasMaxLength(50).IsRequired();
			builder.Property(p => p.OfficeName).HasColumnType("varchar").HasMaxLength(50).IsRequired();

			builder.ToTable("Offices");
		}


	}
}
