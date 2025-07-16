namespace L16_Query_Data_Part_2.Entities
{
	public class Section
	{
		public int Id { get; set; }
		public string SectionName { get; set; }

		public TimeSlot TimeSlot { get; set; }
		public DateRange DateRange { get; set; }

		public int? InstructorId { get; set; }
		public virtual Instructor? Instructor { get; set; }

		public int CourseId { get; set; }
		public virtual Course Course { get; set; }

		public int ScheduleId { get; set; }
		public virtual Schedule Schedule { get; set; }

		public virtual ICollection<Participant> Particpants { get; set; } = new List<Participant>();

		public override string ToString()
		{
			return $"{GetType().Name}: Id={Id}, SectionName={SectionName}, InstructorId={InstructorId}, CourseId={CourseId}, ScheduleId={ScheduleId}";
		}
	}

	public class TimeSlot
	{
		public TimeSpan StartTime { get; set; }
		public TimeSpan EndTime { get; set; }
		public override string ToString()
		{
			return $"TimeSlot: StartTime={StartTime}, EndTime={EndTime}";
		}
	}
	public class DateRange
	{
		public DateOnly StartDate { get; set; }
		public DateOnly EndDate { get; set; }
		public override string ToString()
		{
			return $"DateRange: StartDate={StartDate}, EndDate={EndDate}";
		}
	}
}
