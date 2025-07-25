

using L18_Save_Data.Data;
using L18_Save_Data.Entities;
using L18_Save_Data.Helpers;
using Microsoft.EntityFrameworkCore;

namespace L18_Save_Data
{
	internal class Program
	{
		static void Main(string[] args)
		{

			BasicSave.main();
			ChangeTracking.main();
			CascadeDelete.main();
			EfficientUpdating.main();

		}
	}

	public class BasicSave
	{
		public static void main()
		{
			//RunBasicSave();
			//RunBasicUpdate();
			//RunBasicDelete();
			//RunMultipleOperationsWithSingleSave();
			//RunAddRelatedEntities();
		}

		private static void RunBasicSave()
		{
			DatabaseHelper.RecreateCleanDatabase();

			using var context = new AppDbContext();

			var author = new Author { Id = 1, FName = "test", LName = "1" };

			context.Authors.Add(author);

			context.SaveChanges();
		}

		private static void RunBasicUpdate()
		{


			using var context = new AppDbContext();

			var author = context.Authors.FirstOrDefault(a => a.Id == 1);

			author.FName = "Test";

			context.SaveChanges();
		}

		private static void RunBasicDelete()
		{
			using var context = new AppDbContext();

			var author = context.Authors.FirstOrDefault(a => a.Id == 1);

			context.Authors.Remove(author);

			context.SaveChanges();
		}

		private static void RunMultipleOperationsWithSingleSave()
		{
			using var context = new AppDbContext();

			var author1 = new Author { Id = 1, FName = "Eric", LName = "Evans" };
			context.Authors.Add(author1);

			var author2 = new Author { Id = 2, FName = "John", LName = "Skeet" };
			context.Authors.Add(author2);

			var author3 = new Author { Id = 3, FName = "ditya", LName = "Bhargava" };
			context.Authors.Add(author3);

			author3.FName = "Aditya";

			context.SaveChanges();
		}

		private static void RunAddRelatedEntities()
		{
			using var context = new AppDbContext();

			var author = context.Authors.FirstOrDefault(x => x.Id == 1);

			author.Books.Add(new Book
			{
				Id = 1,
				Title = "Domain-Driven Design: Tackling Complexity in the Heart of Software"
			});

			context.SaveChanges();
		}
	}
	public class ChangeTracking
	{
		public static void main()
		{
			//AsTrackedEntity();
			//AsUnTrackedEntity();

			//Inserting_New_Principal_Author();
			//Inserting_New_Principal_Author_With_Dependent_Book();

			//Attaching_Existing_Author_Principal();

			//Updating_Existing_Author_Principal();

			//Deleting_Existing_Author_Principal();
			//Deleting_Dependent_Book();
		}

		public static void AsTrackedEntity()
		{
			Console.WriteLine($">>>> Sample: {nameof(AsTrackedEntity)}");
			Console.WriteLine();

			DatabaseHelper.RecreateCleanDatabase();
			DatabaseHelper.PopulateDatabase();

			using (var context = new AppDbContext())
			{
				var author = context.Authors.First(); // DB Query using LINQ

				Console.WriteLine(context.ChangeTracker.DebugView.LongView);

				author.FName = "Whatever";
				Console.WriteLine("After Changing FName");
				Console.WriteLine(context.ChangeTracker.DebugView.LongView);

				context.Entry(author).State = EntityState.Modified;
				Console.WriteLine("After Changing State to modified, before save changes");
				Console.WriteLine(context.ChangeTracker.DebugView.LongView);

				context.SaveChanges();
				Console.WriteLine("After Save Changes");
				Console.WriteLine(context.ChangeTracker.DebugView.LongView);

			}

			Console.ReadKey();
		}

		public static void AsUnTrackedEntity()
		{
			Console.WriteLine($">>>> Sample: {nameof(AsUnTrackedEntity)}");
			Console.WriteLine();

			DatabaseHelper.RecreateCleanDatabase();
			DatabaseHelper.PopulateDatabase();

			using (var context = new AppDbContext())
			{
				var author = context.Authors.AsNoTracking().First();
				Console.WriteLine(context.ChangeTracker.DebugView.LongView);
			}

			Console.ReadKey();
		}
		public static void Inserting_New_Principal_Author()
		{
			Console.WriteLine($">>>> Sample: {nameof(Inserting_New_Principal_Author)}");
			Console.WriteLine();

			DatabaseHelper.RecreateCleanDatabase();

			using (var context = new AppDbContext())
			{
				// Mark book  as added
				context.Add(new Author { Id = 1, FName = "Eric", LName = "Evans" });

				Console.WriteLine("Before SaveChanges:");
				Console.WriteLine(context.ChangeTracker.DebugView.LongView);


				context.SaveChanges();

				Console.WriteLine("After SaveChanges:");
				Console.WriteLine(context.ChangeTracker.DebugView.LongView);
			}

			Console.ReadKey();
		}
		public static void Inserting_New_Principal_Author_With_Dependent_Book()
		{
			Console.WriteLine($">>>> Sample: {nameof(Inserting_New_Principal_Author_With_Dependent_Book)}");
			Console.WriteLine();

			DatabaseHelper.RecreateCleanDatabase();

			using (var context = new AppDbContext())
			{
				context.Add(
					new Author
					{
						Id = 1,
						FName = "Eric",
						LName = "Evans",
						Books = new List<Book>
						{
						  new Book
						  {
							  Id = 1,
							  Title = "Domain-Driven Design: Tackling Complexity in the Heart of Software"
						  },
						  new Book
						  {
							  Id = 2,
							  Title = "Domain-Driven Design Reference: Definitions and Pattern Summaries"
						  }
						}
					});

				Console.WriteLine("Before SaveChanges:");
				Console.WriteLine(context.ChangeTracker.DebugView.LongView);


				context.SaveChanges();

				Console.WriteLine("After SaveChanges:");
				Console.WriteLine(context.ChangeTracker.DebugView.LongView);
			}

			Console.ReadKey();
		}

		public static void Attaching_Existing_Author_Principal()
		{
			Console.WriteLine($">>>> Sample: {nameof(Attaching_Existing_Author_Principal)}");
			Console.WriteLine();

			DatabaseHelper.RecreateCleanDatabase();
			DatabaseHelper.PopulateDatabase();

			using (var context = new AppDbContext())
			{
				var author = new Author { Id = 1, FName = "Eric", LName = "Evans" };

				context.Attach(author);

				author.LName = "Evanzzz";

				Console.WriteLine("Before SaveChanges:");
				Console.WriteLine(context.ChangeTracker.DebugView.LongView);

				context.SaveChanges();

				Console.WriteLine("After SaveChanges:");
				Console.WriteLine(context.ChangeTracker.DebugView.LongView);
			}

			Console.ReadKey();
		}

		public static void Updating_Existing_Author_Principal()
		{
			Console.WriteLine($">>>> Sample: {nameof(Updating_Existing_Author_Principal)}");
			Console.WriteLine();

			DatabaseHelper.RecreateCleanDatabase();
			DatabaseHelper.PopulateDatabase();

			using (var context = new AppDbContext())
			{
				// Mark book  as modified
				context.Update(new Author { Id = 1, FName = "EricAAAAA", LName = "Evans" });

				Console.WriteLine("Before SaveChanges:");
				Console.WriteLine(context.ChangeTracker.DebugView.LongView);


				context.SaveChanges();

				Console.WriteLine("After SaveChanges:");
				Console.WriteLine(context.ChangeTracker.DebugView.LongView);
			}

			Console.ReadKey();
		}

		public static void Deleting_Existing_Author_Principal()
		{
			Console.WriteLine($">>>> Sample: {nameof(Deleting_Existing_Author_Principal)}");
			Console.WriteLine();

			DatabaseHelper.RecreateCleanDatabase();
			DatabaseHelper.PopulateDatabase();

			using (var context = new AppDbContext())
			{
				// Mark author as Deleted
				context.Remove(new Author { Id = 1 });

				Console.WriteLine("Before SaveChanges:");
				Console.WriteLine(context.ChangeTracker.DebugView.LongView);


				context.SaveChanges();

				Console.WriteLine("After SaveChanges:");
				Console.WriteLine(context.ChangeTracker.DebugView.LongView);
			}

			Console.ReadKey();
		}
		public static void Deleting_Dependent_Book()
		{
			Console.WriteLine($">>>> Sample: {nameof(Deleting_Dependent_Book)}");
			Console.WriteLine();

			DatabaseHelper.RecreateCleanDatabase();
			DatabaseHelper.PopulateDatabase();

			using (var context = new AppDbContext())
			{
				var book = DatabaseHelper.GetDisconnectedBook();

				context.Attach(book);

				// Mark book  as Deleted
				context.Remove(book);

				Console.WriteLine("Before SaveChanges:");
				Console.WriteLine(context.ChangeTracker.DebugView.LongView);


				context.SaveChanges();

				Console.WriteLine("After SaveChanges:");
				Console.WriteLine(context.ChangeTracker.DebugView.LongView);
			}

			Console.ReadKey();
		}
	}
	public class CascadeDelete
	{
		public static void PrintAuthorsWithBooks()
		{
			using var context = new AppDbContext();

			var res = from b in context.Books
					  join a in context.Authors on b.AuthorId equals a.Id into temp
					  from t in temp.DefaultIfEmpty()
					  select new
					  {
						  BookName = b.Title,
						  AuthorName = t != null ? t.FName + ' ' + t.LName : "<null>"
					  };

			foreach (var item in res)
			{
				Console.WriteLine($"{item.AuthorName}: {item.BookName}");
			}
		}
		public static void PrintAuthorsV2WithBooksV2()
		{
			using var context = new AppDbContext();

			var res = from b in context.BookV2s
					  join a in context.AuthorV2s on b.AuthorV2Id equals a.Id into temp
					  from t in temp.DefaultIfEmpty()
					  select new
					  {
						  BookName = b.Title,
						  AuthorName = t != null ? t.FName + ' ' + t.LName : "<null>"
					  };

			foreach (var item in res)
			{
				Console.WriteLine($"{item.AuthorName}: {item.BookName}");
			}
		}

		public static void main()
		{
			/*
			* Explicit use of OnDelete in EF Core is recommended in the following cases:
			* 
			* 1. When the default cascade behavior is not sufficient for your needs.
			* 2. When you need to enforce custom business logic:
			*    - Example: Prevent deleting an author if they have books assigned;
			*      the books must be reassigned first before allowing deletion.
			*    - These constraints should exist at the database level as well.
			* 3. For performance optimization:
			*    - Example: Deleting an author with millions of related books
			*      could trigger performance issues due to cascading deletes.
			* 
			* In contrast, if the default behavior is acceptable, explicit configuration is not necessary.
			*/


			//DeletePrincipalAuthor_With_Dependent_Book_FK_Required();

			//DeletePrincipalAuthor_With_Dependent_Book_FK_Optional();

			// cut the Relationship without delete must include Dependen

			//SeveringRelationship_DependentBook_SetPrincipal_Null_FK_Optional();

			//SeveringRelationship_PrincipalAuthor_ClearDependents_Dependent_Book_FK_Optional();
		}

		public static void DeletePrincipalAuthor_With_Dependent_Book_FK_Required()
		{
			Console.WriteLine($">>>> Sample: {nameof(DeletePrincipalAuthor_With_Dependent_Book_FK_Required)}");
			Console.WriteLine();

			DatabaseHelper.RecreateCleanDatabase();
			DatabaseHelper.PopulateDatabase();

			using (var context = new AppDbContext())
			{
				Console.WriteLine("before delete");
				PrintAuthorsWithBooks();
				var author = context.Authors.First();


				context.Authors.Remove(author);

				context.SaveChanges();
				Console.WriteLine("after delete");
				PrintAuthorsWithBooks();
			}

			Console.ReadKey();
		}

		public static void DeletePrincipalAuthor_With_Dependent_Book_FK_Optional()
		{
			Console.WriteLine($">>>> Sample: {nameof(DeletePrincipalAuthor_With_Dependent_Book_FK_Optional)}");
			Console.WriteLine();

			DatabaseHelper.RecreateCleanDatabase();
			DatabaseHelper.PopulateDatabase();

			using (var context = new AppDbContext())
			{
				Console.WriteLine("before delete");
				PrintAuthorsV2WithBooksV2();

				// you can set Fk null in dependent with two way
				// 1. include dependent ( context.AuthorV2s.Include(a=>a.BookV2s).First(); )
				// 2. in config set onDeleteto SetNull ( .OnDelete(DeleteBehavior.SetNull); )

				var author = context.AuthorV2s.First();

				context.AuthorV2s.Remove(author);

				context.SaveChanges();
				Console.WriteLine("after delete");
				PrintAuthorsV2WithBooksV2();
			}

			Console.ReadKey();
		}

		public static void SeveringRelationship_DependentBook_SetPrincipal_Null_FK_Optional()
		{
			Console.WriteLine($">>>> Sample: {nameof(SeveringRelationship_DependentBook_SetPrincipal_Null_FK_Optional)}");
			Console.WriteLine();

			DatabaseHelper.RecreateCleanDatabase();
			DatabaseHelper.PopulateDatabase();

			using (var context = new AppDbContext())
			{
				Console.WriteLine("before clear");
				PrintAuthorsV2WithBooksV2();
				var author = context.AuthorV2s.Include(x => x.BookV2s).First();

				author.BookV2s.Clear();

				context.SaveChanges();
				Console.WriteLine("after clear");
				PrintAuthorsV2WithBooksV2();
			}

			Console.ReadKey();
		}

		public static void SeveringRelationship_PrincipalAuthor_ClearDependents_Dependent_Book_FK_Optional()
		{
			Console.WriteLine($">>>> Sample: {nameof(SeveringRelationship_PrincipalAuthor_ClearDependents_Dependent_Book_FK_Optional)}");
			Console.WriteLine();

			DatabaseHelper.RecreateCleanDatabase();
			DatabaseHelper.PopulateDatabase();

			using (var context = new AppDbContext())
			{
				Console.WriteLine("before set null");
				PrintAuthorsV2WithBooksV2();
				var author = context.AuthorV2s.Include(x => x.BookV2s).First();

				foreach (var book in author.BookV2s)
				{
					book.AuthorV2 = null;
				}
				context.SaveChanges();
			}
			Console.WriteLine("after set null");
			PrintAuthorsV2WithBooksV2();
			Console.ReadKey();
		}
	}
	public class EfficientUpdating
	{
		public static void PrintBooks(int id = -1)
		{
			using var context = new AppDbContext();
			if (id <= 0)
			{
				foreach (var book in context.Books)
				{
					Console.WriteLine(book);
				}
			}
			else
			{
				foreach (var book in context.Books.Where(b => b.AuthorId == id))
				{
					Console.WriteLine(book);
				}

			}
		}

		public static void main()
		{
			//Increase_Book_Price_By_10Percent_For_Author1_Typical_Implementation();
			Increase_Book_Price_By_10Percent_For_Author1_EF7AnUp_Implementation();
			Delete_Book_With_Title_Start_With_Book_EF7AnUp_Implementation();
			Increase_Book_Price_By_10Percent_For_Author1_EF7AnUp_RawSql();
		}
		public static void Increase_Book_Price_By_10Percent_For_Author1_Typical_Implementation()
		{
			Console.WriteLine($">>>> Sample: {nameof(Increase_Book_Price_By_10Percent_For_Author1_Typical_Implementation)}");
			Console.WriteLine();

			DatabaseHelper.RecreateCleanDatabase();
			DatabaseHelper.PopulateDatabase();

			using (var context = new AppDbContext())
			{
				Console.WriteLine("before ubdate");
				PrintBooks(1);
				var author1Books = context.Books.Where(x => x.AuthorId == 1);

				foreach (var book in author1Books) // Deffered Execution
				{
					book.Price *= 1.1m;
				}

				context.SaveChanges();
				Console.WriteLine("after ubdate");
				PrintBooks(1);
			}

			Console.ReadKey();
		}
		public static void Increase_Book_Price_By_10Percent_For_Author1_EF7AnUp_Implementation()
		{
			Console.WriteLine($">>>> Sample: {nameof(Increase_Book_Price_By_10Percent_For_Author1_EF7AnUp_Implementation)}");
			Console.WriteLine();

			DatabaseHelper.RecreateCleanDatabase();
			DatabaseHelper.PopulateDatabase();

			using (var context = new AppDbContext())
			{
				Console.WriteLine("before ubdate");
				PrintBooks(1);

				context.Books.Where(b => b.AuthorId == 1)
					.ExecuteUpdate(s => s.SetProperty(b => b.Price, b => b.Price * 1.10m));

				Console.WriteLine("after ubdate");
				PrintBooks(1);
			}

			Console.ReadKey();
		}
		public static void Delete_Book_With_Title_Start_With_Book_EF7AnUp_Implementation()
		{
			Console.WriteLine($">>>> Sample: {nameof(Delete_Book_With_Title_Start_With_Book_EF7AnUp_Implementation)}");
			Console.WriteLine();

			DatabaseHelper.RecreateCleanDatabase();
			DatabaseHelper.PopulateDatabase();

			using (var context = new AppDbContext())
			{
				Console.WriteLine("before delete");
				PrintBooks();
				context.Books.Where(x => x.Title.StartsWith("Book")).ExecuteDelete();
				Console.WriteLine("after delete");
				PrintBooks();
			}

			Console.ReadKey();
		}

		public static void Increase_Book_Price_By_10Percent_For_Author1_EF7AnUp_RawSql()
		{
			Console.WriteLine($">>>> Sample: {nameof(Increase_Book_Price_By_10Percent_For_Author1_EF7AnUp_RawSql)}");
			Console.WriteLine();

			DatabaseHelper.RecreateCleanDatabase();
			DatabaseHelper.PopulateDatabase();

			using (var context = new AppDbContext())
			{
				Console.WriteLine("before ubdate");
				PrintBooks(1);
				context.Database.ExecuteSql($"UPDATE dbo.Books SET Price = Price * 1.1 WHERE AuthorId = 1");
				Console.WriteLine("after ubdate");
				PrintBooks(1);
			}

			Console.ReadKey();
		}
	}


}
