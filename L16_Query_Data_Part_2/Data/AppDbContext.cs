using L16_Query_Data_Part_2.Data.Config;
using L16_Query_Data_Part_2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace L16_Query_Data_Part_2.Data
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


		}
	}
}
