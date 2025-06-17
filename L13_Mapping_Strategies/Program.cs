using L13_Mapping_Strategies.Data;
using L13_Mapping_Strategies.Entities;

namespace L13_Mapping_Strategies;

class Program
{
	static void Main(string[] args)
	{
		// to see Performance https://learn.microsoft.com/en-us/ef/core/performance/modeling-for-performance#inheritance-mapping 

		//BaseTypeDefaultMapping();
		//IncludeAllHeirarchy();
		//TPH();
		//TPT();
		//TPC();

	}

	private static void BaseTypeDefaultMapping()
	{
		// Individual and Coporate not in database because it is not mapping 
	}

	private static void IncludeAllHeirarchy()
	{
		// Individual and Coporate exist in database because it is mapping as Dbset it is use default(TPH)

		using var context = new IncludeAllHeirarchyContext();

		//var p1 = new Individual
		//{
		//	Id = 1,
		//	FName = "test",
		//	LName = "2",
		//	University = "JUST",
		//	YearOfGraduation = 2024,
		//	IsIntern = false
		//};

		//var p2 = new Coporate
		//{
		//	Id = 2,
		//	FName = "test",
		//	LName = "1",
		//	Company = "Metigator",
		//	JobTitle = "Developer"
		//};

		//context.Participants.Add(p1);
		//context.Participants.Add(p2);
		//context.SaveChanges();

		foreach (var item in context.Participants)
		{
			Console.WriteLine(item);
		}
	}

	private static void TPH()
	{
		// TPH: Table Per Hierarchy
		// Maps all classes in the inheritance hierarchy to a single table.
		// A "Discriminator" column is added to distinguish between derived types.
		/* Tip
		 * If your database system supports it (e.g. SQL Server), then consider using "sparse columns" for TPH columns that will be rarely populated.

		 */
		using var context = new TPHContext();

		//var p1 = new Individual
		//{
		//	Id = 1,
		//	FName = "test",
		//	LName = "2",
		//	University = "JUST",
		//	YearOfGraduation = 2024,
		//	IsIntern = false
		//};

		//var p2 = new Coporate
		//{
		//	Id = 2,
		//	FName = "test",
		//	LName = "1",
		//	Company = "Metigator",
		//	JobTitle = "Developer"
		//};

		//context.Participants.Add(p1);
		//context.Participants.Add(p2);
		//context.SaveChanges();

		Console.WriteLine("Individuals:\n");
		foreach (var item in context.Set<Participant>().OfType<Individual>())
		{
			Console.WriteLine(item);
		}
		Console.WriteLine("--------------");
		Console.WriteLine("Coporates:\n");
		foreach (var item in context.Set<Participant>().OfType<Coporate>())
		{
			Console.WriteLine(item);
		}
	}

	private static void TPT()
	{
		// TPT: Table Per Type
		// Maps each class (base and derived) to its own table.
		// Shared properties go in the base table, specific ones in derived tables.
		// Tables are connected using one-to-one relationships.	

		using var context = new TPTContext();

		//var p1 = new Individual
		//{
		//	Id = 1,
		//	FName = "test",
		//	LName = "2",
		//	University = "JUST",
		//	YearOfGraduation = 2024,
		//	IsIntern = false
		//};

		//var p2 = new Coporate
		//{
		//	Id = 2,
		//	FName = "test",
		//	LName = "1",
		//	Company = "Metigator",
		//	JobTitle = "Developer"
		//};

		//context.Participants.Add(p1);
		//context.Participants.Add(p2);
		//context.SaveChanges();

		Console.WriteLine("Individuals:\n");
		foreach (var item in context.Set<Participant>().OfType<Individual>())
		{
			Console.WriteLine(item);
		}
		Console.WriteLine("--------------");
		Console.WriteLine("Coporates:\n");
		foreach (var item in context.Set<Participant>().OfType<Coporate>())
		{
			Console.WriteLine(item);
		}

	}
	private static void TPC()
	{
		// TPC: Table Per Concrete Class
		// Each non-abstract (concrete) class gets its own table.
		// Each table contains all properties (inherited + specific).
		// No table for the base class. No relationships between tables.
	}


}
