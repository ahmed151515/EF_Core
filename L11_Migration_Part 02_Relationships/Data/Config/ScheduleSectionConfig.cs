﻿using L11_Migration_Part_02_Relationships.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace L11_Migration_Part_02_Relationships.Data.Config
{
	public class ScheduleSectionConfig : IEntityTypeConfiguration<ScheduleSection>
	{
		public void Configure(EntityTypeBuilder<ScheduleSection> builder)
		{
			builder.HasKey(c => c.Id);
			builder.Property(p => p.Id).ValueGeneratedNever();

			builder.Property(x => x.StartTime)
				.HasColumnType("time");

			builder.Property(x => x.EndTime)
				.HasColumnType("time");





			builder.HasData(LoadScheduleSections());

			builder.ToTable("SchedulesSections");

		}

		private List<ScheduleSection> LoadScheduleSections()
		=> new List<ScheduleSection>
			{
				new ScheduleSection { Id = 1, SectionId = 1, ScheduleId = 1, StartTime = TimeSpan.Parse("08:00:00"), EndTime = TimeSpan.Parse("10:00:00") },
				new ScheduleSection { Id = 2, SectionId = 2, ScheduleId = 3, StartTime = TimeSpan.Parse("14:00:00"), EndTime = TimeSpan.Parse("18:00:00") },
				new ScheduleSection { Id = 3, SectionId = 3, ScheduleId = 4, StartTime = TimeSpan.Parse("10:00:00"), EndTime = TimeSpan.Parse("15:00:00") },
				new ScheduleSection { Id = 4, SectionId = 4, ScheduleId = 1, StartTime = TimeSpan.Parse("10:00:00"), EndTime = TimeSpan.Parse("12:00:00") },
				new ScheduleSection { Id = 5, SectionId = 5, ScheduleId = 1, StartTime = TimeSpan.Parse("16:00:00"), EndTime = TimeSpan.Parse("18:00:00") },
				new ScheduleSection { Id = 6, SectionId = 6, ScheduleId = 2, StartTime = TimeSpan.Parse("08:00:00"), EndTime = TimeSpan.Parse("10:00:00") },
				new ScheduleSection { Id = 7, SectionId = 7, ScheduleId = 3, StartTime = TimeSpan.Parse("11:00:00"), EndTime = TimeSpan.Parse("14:00:00") },
				new ScheduleSection { Id = 8, SectionId = 8, ScheduleId = 4, StartTime = TimeSpan.Parse("10:00:00"), EndTime = TimeSpan.Parse("14:00:00") },
				new ScheduleSection { Id = 9, SectionId = 9, ScheduleId = 4, StartTime = TimeSpan.Parse("16:00:00"), EndTime = TimeSpan.Parse("18:00:00") },
				new ScheduleSection { Id = 10, SectionId = 10, ScheduleId = 3, StartTime = TimeSpan.Parse("12:00:00"), EndTime = TimeSpan.Parse("15:00:00") },
				new ScheduleSection { Id = 11, SectionId = 11, ScheduleId = 5, StartTime = TimeSpan.Parse("09:00:00"), EndTime = TimeSpan.Parse("11:00:00") }
			};
	}
}
