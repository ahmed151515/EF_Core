using L15_Query_Data_Part_1.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace L15_Query_Data_Part_1.Data.Config
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
