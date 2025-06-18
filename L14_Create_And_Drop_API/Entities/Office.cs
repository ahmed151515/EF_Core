namespace L14_Create_And_Drop_API.Entities
{
	public class Office
	{
		public int Id { get; set; }
		public string OfficeLocation { get; set; }
		public string OfficeName { get; set; }

		public Instructor? Instructor { get; set; }

	}
}
