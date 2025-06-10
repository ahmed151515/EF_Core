using L12_EF_Migration_Part_03.Data;

namespace L12_EF_Migration_Part_03
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
	* Merge Migrations
	*	1.delete Migration folder
	*	2. Add-Migration [name]
	*	3. comment all up and down in your migration
	*	4. delete all Previous __EFMigrationsHistory data
	*	3. Update-Database
	*/

	/*
		Hasdata used in development i deleted becouse we create a base_model migration his has all data 
	 */

	internal class Program
	{
		static void Main(string[] args)
		{
			using var context = new AppDbContext();




			var SectionsTable = from sec in context.Sections
								let sch = sec.Schedule
								select new
								{
									sec.Id,
									sec.Course.CourseName,
									sec.SectionName,
									InstructorName = sec.Instructor != null
									  ? sec.Instructor.FName + " " + sec.Instructor.LName : "–",
									ScheduleTitle = sch.Title,
									sec.TimeSlot.StartTime,
									sec.TimeSlot.EndTime,
									sch.SUN,
									sch.MON,
									sch.TUE,
									sch.WED,
									sch.THU,
									sch.FRI,
									sch.SAT
								};



			Console.WriteLine($"" +
					$"{"Id".PadLeft(3)} | " +
					$"{"Course Name".PadLeft(3)} | " +
					$"{"Section Name".PadLeft(3)} | " +
					$"{"Instructor Name".PadLeft(3)} | " +
					$"{"ScheduleTitle".PadLeft(3)} | " +
					$"{"Start Time".PadLeft(3)} | " +
					$"{"End Time".PadLeft(3)} | " +
					$"{"SUN".PadLeft(3)} | " +
					$"{"MON".PadLeft(3)} | " +
					$"{"TUE".PadLeft(3)} | " +
					$"{"WED".PadLeft(3)} | " +
					$"{"THU".PadLeft(3)} | " +
					$"{"FRI".PadLeft(3)} | " +
					$"{"SAT".PadLeft(3)} | ");
			foreach (var item in SectionsTable)
			{
				Console.WriteLine($"" +
					$"{item.Id.ToString().PadLeft(3)} | " +
					$"{item.CourseName.PadLeft(3)} | " +
					$"{item.SectionName.PadLeft(3)} | " +
					$"{item.InstructorName.PadLeft(3)} | " +
					$"{item.ScheduleTitle.ToString().PadLeft(3)} | " +
					$"{item.StartTime.ToString().PadLeft(3)} | " +
					$"{item.EndTime.ToString().PadLeft(3)} | " +
					$"{item.SUN.ToString().PadLeft(3)} | " +
					$"{item.MON.ToString().PadLeft(3)} | " +
					$"{item.TUE.ToString().PadLeft(3)} | " +
					$"{item.WED.ToString().PadLeft(3)} | " +
					$"{item.THU.ToString().PadLeft(3)} | " +
					$"{item.FRI.ToString().PadLeft(3)} | " +
					$"{item.SAT.ToString().PadLeft(3)} | ");
			}




		}
	}
}
