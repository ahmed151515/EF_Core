using L13_Mapping_Strategies.BaseTypeDefaultMapping.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace L13_Mapping_Strategies.BaseTypeDefaultMapping.Data.Config
{
	public class ParticpantConfig : IEntityTypeConfiguration<Particpant>
	{
		public void Configure(EntityTypeBuilder<Particpant> builder)
		{
			builder.HasKey(c => c.Id);
			builder.Property(p => p.Id).ValueGeneratedNever();

			builder.Property(p => p.FName).HasColumnType("varchar").HasMaxLength(50).IsRequired();
			builder.Property(p => p.LName).HasColumnType("varchar").HasMaxLength(50).IsRequired();




			builder.ToTable("Particpants");
		}



	}
}
