using L16_Query_Data_Part_2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace L16_Query_Data_Part_2.Data.Config
{
	public class SectionConfig : IEntityTypeConfiguration<Section>
	{
		public void Configure(EntityTypeBuilder<Section> builder)
		{
			builder.HasKey(c => c.Id);
			builder.Property(p => p.Id).ValueGeneratedNever();

			builder.Property(p => p.SectionName).HasColumnType("varchar").HasMaxLength(255).IsRequired();


			builder.HasOne(s => s.Course)
					.WithMany(c => c.Sections)
						.HasForeignKey(s => s.CourseId)
							.IsRequired(true);


			builder.HasOne(s => s.Instructor)
					.WithMany(i => i.Sections)
						.HasForeignKey(s => s.InstructorId)
							.IsRequired(false);

			builder.HasOne(s => s.Schedule)
					.WithMany(sch => sch.Sections)
						.HasForeignKey(s => s.ScheduleId)
							.IsRequired();

			builder.HasMany(s => s.Particpants)
					.WithMany(st => st.Sections)
						.UsingEntity<Enrollment>();

			builder.OwnsOne(s => s.TimeSlot, ts =>
			{
				ts.Property(p => p.StartTime).HasColumnType("Time").HasColumnName("StartTime");
				ts.Property(p => p.EndTime).HasColumnType("Time").HasColumnName("EndTime");
			});

			builder.OwnsOne(x => x.DateRange, ts =>
			{
				ts.Property(p => p.StartDate).HasColumnType("date").HasColumnName("StartDate").IsRequired();
				ts.Property(p => p.EndDate).HasColumnType("date").HasColumnName("EndDate").IsRequired();
			});

			builder.ToTable("Sections");

		}

	}

}
