using L14_Create_And_Drop_API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace L14_Create_And_Drop_API.Data.Config
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
