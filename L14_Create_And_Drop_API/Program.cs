
using L14_Create_And_Drop_API.Data;
using Microsoft.EntityFrameworkCore;

namespace L14_Create_And_Drop_API
{
	internal class Program
	{
		static void Main(string[] args)
		{
			//CreateDropAPI();
			//SeedDataModel();
			SeedDataInitializationLogic();
			using var context = new SeedInitLogicContext();




		}

		private static void CreateDropAPI()
		{
			using var context = new CreateDropAPIContext();

			Console.WriteLine("Database created for 30 s");

			context.Database.EnsureCreated();

			var sqlScript = context.Database.GenerateCreateScript();
			// it's timer for 30 s 
			for (int i = 1; i <= 30; i++)
			{
				Console.Write($"\r{i}");
				Thread.Sleep(1000);

			}

			context.Database.EnsureDeleted();
			Console.WriteLine("\nDatabase deleted");
		}


		private static void SeedDataModel()
		{
			// HasData can't work with a OwnsOne
			// you can bypass it by seed it in migration 

		}
		private static void SeedDataInitializationLogic()
		{
			using var context = new SeedInitLogicContext();

			// 1. Discover all DbSet<> properties on the context
			var dbSetProps = context.GetType()
				.GetProperties()
				.Where(p =>
					p.PropertyType.IsGenericType &&
					p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>)
				);

			// 2. Skip any sets you don't want to seed
			var pass = new[] { "Participants" };

			foreach (var prop in dbSetProps)
			{
				if (pass.Contains(prop.Name))
					continue;   // skip excluded sets

				// 3. Get the entity CLR type, e.g. Individual, Course, etc.
				var entityType = prop.PropertyType.GetGenericArguments()[0];

				// 4. Retrieve the DbSet instance
				var dbSet = prop.GetValue(context) as IQueryable;
				if (dbSet == null)
					continue;   // safety check

				// 5. Check if the table already has any data
				var anyMethod = typeof(Queryable)
					.GetMethods()
					.First(m => m.Name == "Any" && m.GetParameters().Length == 1)
					.MakeGenericMethod(entityType);
				var hasData = (bool)anyMethod.Invoke(null, new object[] { dbSet });
				if (hasData)
					continue;   // skip seeding if not empty

				// 6. Load seed data via SeedData.Load{EntityName}()
				var loadMethod = typeof(SeedData)
					.GetMethods()
					.First(m => m.Name == $"Load{prop.Name}");
				var data = loadMethod.Invoke(null, null);

				// 7. Convert List<T> to T[] to match AddRange signature
				var dataArray = data
					.GetType()
					.GetMethod("ToArray", Type.EmptyTypes)
					.Invoke(data, null);

				// 8. Invoke DbSet<T>.AddRange(T[] items)
				var addRangeMethod = typeof(DbSet<>)
					.MakeGenericType(entityType)
					.GetMethods()
					.First(m => m.Name == "AddRange" && m.GetParameters().Length == 1);
				addRangeMethod.Invoke(prop.GetValue(context), new[] { dataArray });
			}

			// 9. Persist all changes to the database
			context.SaveChanges();
		}


	}
}

