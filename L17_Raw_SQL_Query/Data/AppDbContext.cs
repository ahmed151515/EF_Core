using L17_Raw_SQL_Query.Data.Config;
using L17_Raw_SQL_Query.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace L17_Raw_SQL_Query.Data
{
	public class AppDbContext : DbContext
	{

		public DbSet<Course> Courses { get; set; }
		public DbSet<Instructor> Instructors { get; set; }
		public DbSet<Office> Offices { get; set; }
		public DbSet<Section> Sections { get; set; }
		public DbSet<Schedule> Schedules { get; set; }
		public DbSet<Participant> Participants { get; set; }
		public DbSet<Individual> Individuals { get; set; }
		public DbSet<Coporate> Coporates { get; set; }
		public DbSet<Enrollment> Enrollments { get; set; }
		public DbSet<Review> Reviews { get; set; }
		public DbSet<SectionDetails> sectionDetails { get; set; }
		public DbSet<CourseOverview> CourseOverviews { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
			var constr = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json").Build()
				.GetSection("constr").Value;
			optionsBuilder          // .UseLazyLoadingProxies()
				.UseSqlServer(constr
				//,d=>d.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
				) // .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
				.LogTo(Console.WriteLine, LogLevel.Information)
				;


		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfigurationsFromAssembly(typeof(CourseConfig).Assembly);

			modelBuilder.HasDbFunction(
				typeof(AppDbContext).GetMethod(nameof(SectionsExceedingParticipantCount), new[] { typeof(int) }))
				.HasName("GetSectionsExceedingParticipantCount");
		}


		[DbFunction("fn_InstructorAvailability", Schema = "dbo")]
		public static string GetInstructorAvailability(
			int instructorId,
			DateTime startDate,
			DateTime endDate,
			TimeSpan startTime,
			TimeSpan endTime
			)
		{
			// This doesn't need an implementation; EF core uses the function mapping
			throw new NotImplementedException();
		}


		public IQueryable<Section> SectionsExceedingParticipantCount(
			int numberOfParticipants
			) => FromExpression(() => SectionsExceedingParticipantCount(numberOfParticipants));


	}
}
