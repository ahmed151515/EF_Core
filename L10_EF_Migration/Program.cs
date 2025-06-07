using L10_EF_Migration.Data;

namespace L10_EF_Migration
{
	/*
	* Create a Migration
	*      PMC: Add-Migration [name]
	*      Terminal: dotnet ef migrations add [name]
	*  
	* Remove a Migration
	*      PMC: Remove-Migration
	*      Terminal: dotnet ef migrations remove
	*  
	* Apply Migrations to Database
	*      PMC: Update-Database
	*      Terminal: dotnet ef database update
	*/

	internal class Program
	{
		static void Main(string[] args)
		{
			using var context = new AppDbContext();

			foreach (var item in context.Courses)
			{
				Console.WriteLine(item.CourseName);
			}
		}
	}
}
