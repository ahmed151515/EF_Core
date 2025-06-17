using L13_Mapping_Strategies.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace L13_Mapping_Strategies.Data.Config
{
	public class ParticipantConfig : IEntityTypeConfiguration<Participant>
	{
		public void Configure(EntityTypeBuilder<Participant> builder)
		{
			builder.HasKey(c => c.Id);
			builder.Property(p => p.Id).ValueGeneratedNever();

			builder.Property(p => p.FName).HasColumnType("varchar").HasMaxLength(50).IsRequired();
			builder.Property(p => p.LName).HasColumnType("varchar").HasMaxLength(50).IsRequired();




			builder.ToTable("Participants");
		}



	}
}
