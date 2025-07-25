namespace L18_Save_Data.Entities
{
	public class AuthorV2
	{
		public int Id { get; set; }
		public string FName { get; set; }
		public string LName { get; set; }

		public List<BookV2> BookV2s { get; set; } = new();
	}

}
