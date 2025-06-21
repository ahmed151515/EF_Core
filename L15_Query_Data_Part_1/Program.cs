
using L15_Query_Data_Part_1.Data;
using Microsoft.EntityFrameworkCore;

namespace L15_Query_Data_Part_1
{
	internal class Program
	{
		static void Main(string[] args)
		{
			//queryData();
			//clientVsServerEvaluation();
			//trackingVsNoTracking();
			//relatedDataEager();
			//relatedDataExplicitLoading();
			relatedDataLazyLoading();

		}

		private static void queryData()
		{
			using var context = new AppDbContext();

			//var courses = context.Courses;

			// Console.WriteLine(courses.ToQueryString()); // for debuging and available in .net 5

			//foreach (var item in courses)
			//{
			//	Console.WriteLine(item);
			//}


			//var course = context.Courses.Single(c => c.Id == 1);

			//Console.WriteLine(course);

			var courses = context.Courses.Where(c => c.Price > 2000 && c.Price < 3000);

			Console.WriteLine(courses.ToQueryString()); // for debuging and it's available in .net 5

			foreach (var item in courses)
			{
				Console.WriteLine(item);
			}
		}

		private static void clientVsServerEvaluation()
		{
			using var context = new AppDbContext();

			var courseId = 1;

			var sections = context.Sections.Where(s => s.CourseId == courseId)
									.Select(s => new
									{
										SectionId = s.Id,
										SectionName = s.SectionName.Substring(4),
										TotalDays = CalculateToTotalDays(s.DateRange.StartDate, s.DateRange.EndDate)
										//TotalDays = EF.Functions.DateDiffDay(s.DateRange.StartDate, s.DateRange.EndDate)
									});



			Console.WriteLine(sections.ToQueryString());
			Console.WriteLine();

			foreach (var item in sections)
			{
				Console.WriteLine(item);
			}
		}

		private static int CalculateToTotalDays(DateOnly startDate, DateOnly endDate)
		{
			return endDate.DayNumber - startDate.DayNumber;
		}

		private static void trackingVsNoTracking()
		{
			// by default EF core tracking all queries results

			using var context = new AppDbContext();

			//var section = context.Sections.First(s => s.Id == 1);

			//Console.WriteLine("before changeing tracked entity");
			//Console.WriteLine(section.SectionName);

			//section.SectionName = "changed";
			//context.SaveChanges();

			//section = context.Sections.First(s => s.Id == 1);
			//Console.WriteLine("after changed");
			//Console.WriteLine(section.SectionName);


			var section = context.Sections.AsNoTracking().First(s => s.Id == 1);

			Console.WriteLine("before changeing tracked entity");
			Console.WriteLine(section.SectionName);

			section.SectionName = "Not changed";
			context.SaveChanges();

			section = context.Sections.First(s => s.Id == 1);
			Console.WriteLine("after changed");
			Console.WriteLine(section.SectionName);

			/*
			 * use NoTracking if you want
			 * data readonly
			 * fast result without edit
			 */
			// you can change the Tracking Behavior of context in OnConfiguring like
			// optionsBuilder.UseSqlServer(constr).UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
		}

		private static void relatedDataEager()
		{
			using var context = new AppDbContext();


			var sections = context.Sections.Include(s => s.Particpants).Where(s => s.Id == 1);
			Console.WriteLine("-------------");
			Console.WriteLine(sections.ToQueryString());
			Console.WriteLine("-------------");

			var section = sections.FirstOrDefault();


			foreach (var item in section.Particpants)
			{
				Console.WriteLine(item);
			}

			Console.WriteLine("-------------");
			sections = context.Sections.Include(s => s.Instructor)
							.ThenInclude(i => i.Office).Where(s => s.Id == 1);

			Console.WriteLine(sections.ToQueryString());

			section = sections.FirstOrDefault();


			Console.WriteLine("-------------");

			var instructor = section.Instructor;
			var office = section.Instructor.Office;

			Console.WriteLine(instructor);
			Console.WriteLine(office);


		}

		private static void relatedDataExplicitLoading()
		{
			using var context = new AppDbContext();

			var sections = context.Sections.Where(s => s.Id == 1);



			var section = sections.FirstOrDefault();

			var query = context.Entry(section).Collection(s => s.Particpants).Query();

			Console.WriteLine("-------------");
			Console.WriteLine(query.ToQueryString());
			Console.WriteLine("-------------");

			foreach (var item in query)
			{
				Console.WriteLine(item);
			}

			Console.WriteLine("-------------");
			sections = context.Sections.Where(s => s.Id == 1);


			section = sections.FirstOrDefault();

			context.Entry(section).Reference(s => s.Instructor).Load();
			context.Entry(section.Instructor).Reference(i => i.Office).Load();

			var q2 = context.Entry(section).Reference(s => s.Instructor).Query();
			var q3 = context.Entry(section.Instructor).Reference(i => i.Office).Query();

			Console.WriteLine("-------------");
			Console.WriteLine(q2.ToQueryString());
			Console.WriteLine("-------------");
			Console.WriteLine(q3.ToQueryString());

			Console.WriteLine("-------------");

			var instructor = section.Instructor;
			var office = section.Instructor.Office;

			Console.WriteLine(instructor);
			Console.WriteLine(office);
		}

		private static void relatedDataLazyLoading()
		{
			// the EF core load the navigation properties if you access it

			// setup whith proxies
			// 1. donwload nuget package Microsoft.EntityFrameworkCore.Proxies
			// 2. in AppDbContext in OnConfiguring add .UseLazyLoadingProxies()
			// 3. make all navigation properties virtual

			// how use ILazyLoader 
			// see https://www.learnentityframeworkcore.com/lazy-loading#ilazyloader
			using var context = new AppDbContext();

			var sections = context.Sections.Where(s => s.Id == 1);



			var section = sections.FirstOrDefault();




			foreach (var item in section.Particpants)
			{
				Console.WriteLine(item);
			}

			Console.WriteLine("-------------");
			sections = context.Sections.Where(s => s.Id == 1);


			section = sections.FirstOrDefault();



			Console.WriteLine("-------------");

			var instructor = section.Instructor;
			var office = section.Instructor.Office;

			Console.WriteLine(instructor);
			Console.WriteLine(office);
		}
	}
}
