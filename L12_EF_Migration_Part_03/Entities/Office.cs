namespace L12_EF_Migration_Part_03.Entities
{
	public class Office
	{
		public int Id { get; set; }
		public string OfficeLocation { get; set; }
		public string OfficeName { get; set; }

		public Instructor? Instructor { get; set; }

	}
}
