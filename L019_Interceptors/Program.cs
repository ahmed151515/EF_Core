using L19_Interceptors.Data;
using L19_Interceptors.Helpers;

namespace L019_Interceptors
{
	internal class Program
	{
		static void Main(string[] args)
		{

			/*
			 * There are two types of delete strategies in systems:
			 * 
			 * 1. Hard Delete: Physically removes the record from the database.
			 * 2. Soft Delete: Marks the entity as deleted (e.g., using a boolean flag),
			 *    but keeps the record in the database for auditing or recovery purposes.
			 */

			DatabaseHelper.RecreateCleanDatabase();
			DatabaseHelper.PopulateDatabase();

			Console.WriteLine();
			Console.WriteLine("Before Delete");

			DatabaseHelper.ShowBooks();

			using (var context = new AppDbContext())
			{
				var book = context.Books.First();
				context.Books.Remove(book);
				context.SaveChanges();
			}

			Console.WriteLine();
			Console.WriteLine("After Delete Book Id = '1'");

			DatabaseHelper.ShowBooks();

			// use global filter to hide deleted books
			Console.ReadKey();
		}
	}
}
