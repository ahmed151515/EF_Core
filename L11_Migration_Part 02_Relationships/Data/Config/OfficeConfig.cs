using L11_Migration_Part_02_Relationships.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace L11_Migration_Part_02_Relationships.Data.Config
{
	public class OfficeConfig : IEntityTypeConfiguration<Office>
	{
		public void Configure(EntityTypeBuilder<Office> builder)
		{
			builder.HasKey(c => c.Id);
			builder.Property(p => p.Id).ValueGeneratedNever();

			builder.Property(p => p.OfficeLocation).HasColumnType("varchar").HasMaxLength(50).IsRequired();
			builder.Property(p => p.OfficeName).HasColumnType("varchar").HasMaxLength(50).IsRequired();

			builder.HasData(LoadOffices());

			builder.ToTable("Offices");
		}

		private Office[] LoadOffices() => new Office[]
			{

				 new Office { Id = 1, OfficeName = "Off_05", OfficeLocation = "building A"},
				new Office { Id = 2, OfficeName = "Off_12", OfficeLocation = "building B"},
				new Office { Id = 3, OfficeName = "Off_32", OfficeLocation = "Adminstration"},
				new Office { Id = 4, OfficeName = "Off_44", OfficeLocation = "IT Department"},
				new Office { Id = 5, OfficeName = "Off_43", OfficeLocation = "IT Department"},
			};
	}
}
