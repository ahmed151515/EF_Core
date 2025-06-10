namespace L11_Migration_Part_02_Relationships.Entities
{
	public class Student
	{
		public int Id { get; set; }

		public string? FName { get; set; }

		public string? LName { get; set; }


		public ICollection<Section> Sections { get; set; } = new List<Section>();

	}


}
