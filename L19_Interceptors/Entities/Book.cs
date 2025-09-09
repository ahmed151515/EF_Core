using L19_Interceptors.Entities.Contract;

namespace L19_Interceptors.Entities
{
	public class Book : ISoftDeleteable
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Author { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime? DeleteDate { get; set; }
	}

}
