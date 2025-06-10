using L12_EF_Migration_Part_03.Entities;
using L12_EF_Migration_Part_03.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace L12_EF_Migration_Part_03.Data.Config
{
	public class ScheduleConfig : IEntityTypeConfiguration<Schedule>
	{
		public void Configure(EntityTypeBuilder<Schedule> builder)
		{
			builder.HasKey(c => c.Id);
			builder.Property(p => p.Id).ValueGeneratedNever();

			builder.Property(p => p.Title).HasColumnType("varchar").HasMaxLength(255).IsRequired();

			builder.Property(p => p.Title)
					.HasConversion(t => t.ToString(), t => (ScheduleEnum)Enum.Parse(typeof(ScheduleEnum), t));

			builder.ToTable("Schedules");

		}

	}
}
