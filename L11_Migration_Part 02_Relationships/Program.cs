using L11_Migration_Part_02_Relationships.Data;

namespace L11_Migration_Part_02_Relationships
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
				Console.WriteLine($"{item.Id} {item.CourseName} {item.Price}");
			}
			Console.WriteLine("---------------------");
			foreach (var item in context.Offices)
			{
				Console.WriteLine($"{item.Id} {item.OfficeName} {item.OfficeLocation}");
			}
			Console.WriteLine("---------------------");

			var InstructorsTable = from ins in context.Instructors
								   let off = ins.Office
								   select new
								   {
									   ins.Id,
									   InstructorName = $"{ins.FName} {ins.LName}",
									   off.OfficeName
								   };
			foreach (var item in InstructorsTable)
			{
				Console.WriteLine($"{item.Id} {item.InstructorName} {item.OfficeName}");
			}
			Console.WriteLine("---------------------");








			var SectionsTable = from ss in context.ScheduleSections
								let sec = ss.Section
								let sch = ss.Schedule
								select new
								{
									Id = ss.Id,
									CourseName = sec.Course.CourseName,
									SectionName = sec.SectionName,
									InstructorName = sec.Instructor != null
									  ? sec.Instructor.FName + " " + sec.Instructor.LName : "–",
									ScheduleTitle = sch.Title,
									StartTime = ss.StartTime,
									EndTime = ss.EndTime,
									sch.SUN,
									sch.MON,
									sch.TUE,
									sch.WED,
									sch.THU,
									sch.FRI,
									sch.SAT
								};



			Console.WriteLine($"" +
					$"{"Id".PadLeft(5)} " +
					$"{"Course Name".PadLeft(5)} " +
					$"{"Section Name".PadLeft(5)} " +
					$"{"Instructor Name".PadLeft(5)} " +
					$"{"ScheduleTitle".PadLeft(5)} " +
					$"{"Start Time".PadLeft(5)} " +
					$"{"End Time".PadLeft(5)} " +
					$"{"SUN".PadLeft(5)} " +
					$"{"MON".PadLeft(5)} " +
					$"{"TUE".PadLeft(5)} " +
					$"{"WED".PadLeft(5)} " +
					$"{"THU".PadLeft(5)} " +
					$"{"FRI".PadLeft(5)} " +
					$"{"SAT".PadLeft(5)} ");
			foreach (var item in SectionsTable)
			{
				Console.WriteLine($"" +
					$"{item.Id.ToString().PadLeft(5)} " +
					$"{item.CourseName.PadLeft(5)} " +
					$"{item.SectionName.PadLeft(5)} " +
					$"{item.InstructorName.PadLeft(5)} " +
					$"{item.ScheduleTitle.PadLeft(5)} " +
					$"{item.StartTime.ToString().PadLeft(5)} " +
					$"{item.EndTime.ToString().PadLeft(5)} " +
					$"{item.SUN.ToString().PadLeft(5)} " +
					$"{item.MON.ToString().PadLeft(5)} " +
					$"{item.TUE.ToString().PadLeft(5)} " +
					$"{item.WED.ToString().PadLeft(5)} " +
					$"{item.THU.ToString().PadLeft(5)} " +
					$"{item.FRI.ToString().PadLeft(5)} " +
					$"{item.SAT.ToString().PadLeft(5)} ");
			}
			Console.WriteLine("---------------------");

			var StudentEnrollment = from se in context.Enrollments
									let st = se.Student
									let sec = se.Section
									orderby st.Id
									select new
									{
										StudentId = st.Id,
										StudentName = $"{st.FName} {st.LName}",
										sec.SectionName
									};

			foreach (var item in StudentEnrollment)
			{
				Console.WriteLine($"{item.StudentId.ToString().PadLeft(10)} {item.StudentName.PadLeft(10)} {item.SectionName.PadLeft(10)}");
			}
		}
	}
}
