namespace L16_Query_Data_Part_2.Entities
{
	public class Review
	{
		public int Id { get; set; }

		public string Feedback { get; set; }

		public int CourseId { get; set; }

		public Course Course { get; set; }

		public DateTime CreatedAt { get; set; }

		public override string ToString()
		{
			return $"Review Id: {Id}, Feedback: {Feedback}, CourseId: {CourseId}, CreatedAt: {CreatedAt:yyyy-MM-dd HH:mm}, Course: {(Course != null ? Course.CourseName : "null")}";
		}
	}
}
