using L17_Raw_SQL_Query.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace L17_Raw_SQL_Query
{
	internal class Program
	{
		static void Main(string[] args)
		{
			// 1. FromSql EF 7.0
			// 2. FromSqlInterpolated EF 3.0
			// 3. FromSqlRaw EF 3.0
			using var context = new AppDbContext();

			//sqlQuery(context);
			//sqlQueryParamter(context);
			//callingStoredProcedure(context);
			//callingDbView(context);
			//callingUserDefinedFunction(context);
			//callingTableValuedFunction(context);
			globalQueryFilter(context);

		}

		private static void sqlQuery(AppDbContext context)
		{
			var courses = context.Courses.FromSql($"select * from Courses").ToList();
			var courses2 = context.Courses.FromSqlInterpolated($"select * from Courses").ToList();
			var courses3 = context.Courses.FromSqlRaw("select * from Courses").ToList();

			//foreach (var item in courses)
			//{
			//    Console.WriteLine(item.CourseName);
			//}
		}

		private static void sqlQueryParamter(AppDbContext context)
		{
			var id = "1; delete from courses";

			var course = context.Courses.FromSql($"select * from Courses where id={1}").AsEnumerable().SingleOrDefault();
			Console.WriteLine(course);

			var course1 = context.Courses.FromSqlInterpolated($"select * from Courses where id={1}").AsEnumerable().SingleOrDefault();
			Console.WriteLine(course1);

			var idParam = new SqlParameter("@id", 1);
			// not use paramters
			var course2 = context.Courses.FromSqlRaw($"select * from Courses where id=@id", new object[] { idParam }).AsEnumerable().SingleOrDefault();
			Console.WriteLine(course2);

		}

		private static void callingStoredProcedure(AppDbContext context)
		{
			var startDateParam = new SqlParameter("@StartDate", System.Data.SqlDbType.Date)
			{
				Value = new DateTime(2023, 01, 01)
			};
			var endDateParam = new SqlParameter("@EndDate", System.Data.SqlDbType.Date)
			{
				Value = new DateTime(2023, 06, 30)
			};

			var sections = context.sectionDetails
				.FromSql($"Exec dbo.sp_GetSectionWithninDateRange {startDateParam}, {endDateParam}")
				.ToList();


			foreach (var s in sections)
			{
				Console.WriteLine(s);
			}
		}

		private static void callingDbView(AppDbContext context)
		{
			var coursesOverviews = context.CourseOverviews.ToList();
			foreach (var courseOverview in coursesOverviews)
			{
				Console.WriteLine(courseOverview);
			}
		}


		private static void callingUserDefinedFunction(AppDbContext context)
		{
			var startDate = new DateTime(2023, 09, 24);
			var endDate = new DateTime(2023, 12, 26);
			var startTime = new TimeSpan(08, 00, 00);
			var endTime = new TimeSpan(11, 00, 00);

			var res = context.Instructors.Select(i => new
			{
				//Name = i.FName + " " + i.LName,
				i.FullName,
				DateRange = $"{startDate.ToShortDateString()}-{endDate.ToShortDateString()}",
				TimeRange = $"{startTime.ToString("hh\\:mm")}-{endTime.ToString("hh\\:mm")}",
				Status = AppDbContext.GetInstructorAvailability(i.Id, startDate, endDate, startTime, endTime)
			});

			foreach (var item in res)
			{
				Console.WriteLine(item);
			}
		}

		private static void callingTableValuedFunction(AppDbContext context)
		{
			var res = context.SectionsExceedingParticipantCount(10);

			foreach (var item in res)
			{
				Console.WriteLine(item);
			}
		}

		private static void globalQueryFilter(AppDbContext context)
		{
			foreach (var item in context.Sections)
			{
				Console.WriteLine(item);
			}
		}
	}
}
