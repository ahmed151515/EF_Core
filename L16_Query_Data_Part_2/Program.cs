
using L16_Query_Data_Part_2.Data;
using L16_Query_Data_Part_2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace L16_Query_Data_Part_2
{
	internal class Program
	{
		static void Main(string[] args)
		{
			//splitQueries();
			//innerJoin();
			//leftJoin();
			//crossJoin();
			//selectMany();
			//groupBy();
			pagination();

		} 

		private static void splitQueries()
		{
            /*
			 * singal query
			 * pros:
			 * data consistency (concurrency)
			 * 1 request
			 * 
			 * cons:
			 * cortination explosion
			 * Duplicate data
			 * 
			 * -----
			 * split query
			 * 
			 * pros: 
			 * no cortination explosion
			 * no Duplicate data
			 * 
			 * cons:
			 * many requests 
			 * use more memory (memory buffering)
			 * 
			 * 
			 * https://learn.microsoft.com/en-us/ef/core/querying/single-split-queries
			 */

            using var context = new AppDbContext();
			
			var courses1 = context.Courses.AsNoTracking()
				.Include(e => e.Sections)
				.Include(e => e.Reviews)
				.ToList();
            Console.WriteLine("----------------");

            // proper projection(select) reduce network traffic 
            var courses2 = context.Courses.AsNoTracking().Select(c => new
			{
				CourseId = c.Id,
				c.CourseName,
				Hours = c.HoursToComplete,
				Sections = c.Sections.Select(s => new
				{
					SectionId = s.Id,
					s.SectionName,
					dataRate = s.DateRange.ToString()
				}),
				Reviews = c.Reviews.Select(r => new
				{
					r.Id,
					r.Feedback,
					r.CreatedAt
                })
			}).ToList();
            Console.WriteLine("----------------");
			var courses3 = context.Courses.AsNoTracking()
				.Include(e => e.Sections)
				.Include(e => e.Reviews)
				.AsSplitQuery()
                .ToList();
            Console.WriteLine("----------------");

            var courses = context.Courses.AsSplitQuery().AsNoTracking()
				
				.Select(c => new
				{
					CourseId = c.Id,
					c.CourseName,
					Hours = c.HoursToComplete,
					Sections = c.Sections.Select(s => new
					{
						SectionId = s.Id,
						s.SectionName,
						dataRate = s.DateRange.ToString()
					}),
					Reviews = c.Reviews.Select(r => new
					{
						r.Id,
						r.Feedback,
						r.CreatedAt
					})
				})
				.ToList();
			
        }

		private static void innerJoin()
		{
            using var context = new AppDbContext();

			var result = context.Courses.Join(context.Sections, c => c.Id, s => s.CourseId, (c,s)=> new
			{
				CouresId= c.Id,
				c.CourseName,
				SectioId=s.Id,
				s.SectionName
			});

			int i = 0;
            foreach (var item in result)
            {
                Console.Write($"[{i}]\t");
                Console.WriteLine(item);
				i++;
            }

        }

        private static void leftJoin()
		{
            using var context = new AppDbContext();
			var result = from o in context.Offices
						 join ins in context.Instructors
						 on o.Id equals ins.OfficeId into temp
						 from t in temp.DefaultIfEmpty()
						 select new
						 {
							 o.OfficeName,

                             Instructor = t != null ? $"{t.FName} {t.LName}": "Empty"
                         };

            int i = 0;
            foreach (var item in result)
            {
                Console.Write($"[{i}]\t");
                Console.WriteLine(item);
                
                i++;
            }
            //var officeOccupancyMethodSyntax = context.Offices
            //   .GroupJoin(
            //       context.Instructors,
            //       o => o.Id,
            //       i => i.OfficeId,
            //       (office, instructor) => new { office, instructor }
            //   )
            //   .SelectMany(
            //       ov => ov.instructor.DefaultIfEmpty(),
            //       (ov, instructor) => new
            //       {
            //           OfficeId = ov.office.Id,
            //           Name = ov.office.OfficeName,
            //           Location = ov.office.OfficeLocation,
            //           Instructor = instructor != null ? instructor.FullName : "<<EMPTY>>"
            //       }
            //   ).ToList();

            //foreach (var office in officeOccupancyMethodSyntax)
            //{
            //    Console.WriteLine($"{office.Name} -> {office.Instructor}");
            //}
        }
        

        private static void crossJoin()
		{
            using var context = new AppDbContext();

			var res = from i in context.Instructors
					  from s in context.Sections
					  select new
					  {
						  i.FullName,
						  s.SectionName
					  };


            Console.WriteLine(res.Count());

			var sectionInstructorMethodSyntax = context.Sections
				.SelectMany(
				s=>context.Instructors,
				(s,i)=> new {s.SectionName,i.FullName}
				).ToList();

			Console.WriteLine(sectionInstructorMethodSyntax.Count());

		}

        private static void selectMany()
		{
            using var context = new AppDbContext();
            // all student of front
            var res = context.Courses
					.Where(c => c.CourseName.Contains("Frontend") )
					.SelectMany(c => c.Sections, (c, s) => new { c.CourseName, s })
					.SelectMany(s => s.s.Particpants, (s, p) => new { s, p })
					.Select(r => new
					{
						r.s.CourseName,
						r.s.s.SectionName,
						ParticpantName = $"{r.p.FName} {r.p.LName}"
					});


            foreach (var item in res)
            {
                Console.WriteLine(item);
            }
        }

        private static void groupBy()
		{
            using var context = new AppDbContext();

			// get all instructors Coures

			var res =
			from s in context.Sections
			group s by s.Instructor into g

			select new
			{
				Key = g.Key,
				Sections = g.Count(),
				//Sections = g.ToList(),
				
			};
     //       var res =
					//from s in context.Sections
					//group s.Course by s.Instructor;
            foreach (var item in res)
            {
				Console.WriteLine($"instructor :{item.Key.FullName}");
				Console.WriteLine($"Sections {item.Sections}");
				//foreach (var item1 in item.Sections)
				//{
				//	Console.WriteLine($"\t {item1.SectionName}");
				//}
			}
        }

        private static void pagination()
		{
            using var context = new AppDbContext();

			var pageNumber = 1;
			var pageSize = 10;
			var totalSections = context.Sections.Count();
			var totalPages = (int)Math.Ceiling((double)totalSections / pageSize);

			var query = context.Sections.AsNoTracking()
					.Include(x => x.Course)
					.Include(x => x.Instructor)
					.Include(x => x.Schedule)
					.Select(x =>
					new
					{
						Course = x.Course.CourseName,
						Instructor = x.Instructor.FullName,
						DateRange = x.DateRange.ToString(),
						TimeSlot = x.TimeSlot.ToString(),
						Days = string.Join(" ",   // "SAT SUN MON"
							   x.Schedule.SUN ? "SUN" : "",
							   x.Schedule.SAT ? "SAT" : "",
							   x.Schedule.MON ? "MON" : "",
							   x.Schedule.TUE ? "TUE" : "",
							   x.Schedule.WED ? "WED" : "",
							   x.Schedule.THU ? "THU" : "",
							   x.Schedule.FRI ? "FRI" : "")
					});

			Console.WriteLine("|           Course                   |          Instructor            |       Date Range        |   Time Slot   |            Days                |");
			Console.WriteLine("|------------------------------------|--------------------------------|-------------------------|---------------|--------------------------------|");
			while (pageNumber <= totalPages)
			{
				// actual paging
				var pageResult = query.Skip(pageNumber - 1).Take(pageSize);


				foreach (var section in pageResult)
				{
					Console.WriteLine($"| {section.Course,-34} | {section.Instructor,-30} | {section.DateRange.ToString(),-23} | {section.TimeSlot.ToString(),-12} | {section.Days,-30} |");
				}

				Console.WriteLine();

				for (int p = 1; p <= totalPages; p++)
				{
					Console.ForegroundColor = p == pageNumber ? ConsoleColor.Yellow : ConsoleColor.DarkGray;
					Console.Write($"{p} "); // 1 2 3 4 5 .... 20
				}
				Console.ForegroundColor = ConsoleColor.White;

				var page = Console.ReadKey();
				if (page.Key == ConsoleKey.RightArrow && pageNumber < totalPages) 
				{
					pageNumber++; 
				}
				else if (page.Key == ConsoleKey.LeftArrow && pageNumber >= 1) 
				{ 
					pageNumber--; 
				}
                Console.Clear();
			}

		}
    

    }
}
