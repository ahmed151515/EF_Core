﻿namespace L18_Save_Data.Entities
{
	public class Book
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public decimal Price { get; set; }

		public int AuthorId { get; set; }
		public Author Author { get; set; }
		public override string ToString()
		{
			return $"Book: Id={Id}, Title={Title}, Price={Price}, AuthorId={AuthorId}";
		}
	}

}
