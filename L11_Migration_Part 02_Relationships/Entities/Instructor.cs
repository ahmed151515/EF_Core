namespace L11_Migration_Part_02_Relationships.Entities
{
	public class Instructor
	{
		public int Id { get; set; }
		public string FName { get; set; }
		public string LName { get; set; }

		public int? OfficeId { get; set; }
		public Office? Office { get; set; }

		public ICollection<Section> Sections { get; set; } = new List<Section>();

	}
}
