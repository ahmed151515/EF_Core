namespace L15_Query_Data_Part_1.Entities
{
	public class Course
	{
		public int Id { get; set; }
		public string CourseName { get; set; }
		public decimal Price { get; set; }
		public int HoursToComplete { get; set; }

		public virtual ICollection<Section> Sections { get; set; } = new List<Section>();

		public override string ToString()
		{
			return $"{GetType().Name}: Id={Id}, CourseName={CourseName}, Price={Price}, HoursToComplete={HoursToComplete}";
		}
	}
}
