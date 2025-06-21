using L15_Query_Data_Part_1.Entities;
using L15_Query_Data_Part_1.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace L15_Query_Data_Part_1.Data.Config
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
