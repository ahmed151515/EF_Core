namespace L11_Migration_Part_02_Relationships.Entities
{
	public class Section
	{
		public int Id { get; set; }
		public string SectionName { get; set; }

		public int? InstructorId { get; set; }
		public Instructor? Instructor { get; set; }


		public int CourseId { get; set; }
		public Course Course { get; set; }



		public ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
		public ICollection<ScheduleSection> ScheduleSections { get; set; } = new List<ScheduleSection>();

		public ICollection<Student> Students { get; set; } = new List<Student>();


	}


}
