namespace L13_Mapping_Strategies.BaseTypeDefaultMapping.Entities
{
	public class Section
	{
		public int Id { get; set; }
		public string SectionName { get; set; }

		public TimeSlot TimeSlot { get; set; }

		public int? InstructorId { get; set; }
		public Instructor? Instructor { get; set; }


		public int CourseId { get; set; }
		public Course Course { get; set; }



		public int ScheduleId { get; set; }
		public Schedule Schedule { get; set; }

		public ICollection<Particpant> Particpants { get; set; } = new List<Particpant>();


	}

	public class TimeSlot
	{
		public TimeSpan StartTime { get; set; }
		public TimeSpan EndTime { get; set; }
	}
}
