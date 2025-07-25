namespace L18_Save_Data.Entities
{
	public class BookV2
	{
		public int Id { get; set; }
		public string Title { get; set; }

		public int? AuthorV2Id { get; set; } // FK
		public AuthorV2? AuthorV2 { get; set; }
	}

}
