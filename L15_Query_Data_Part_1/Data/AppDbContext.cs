using L15_Query_Data_Part_1.Data.Config;
using L15_Query_Data_Part_1.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace L15_Query_Data_Part_1.Data
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


		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
			var constr = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json").Build()
				.GetSection("constr").Value;
			optionsBuilder.UseLazyLoadingProxies()
				.UseSqlServer(constr) // .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
				;
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfigurationsFromAssembly(typeof(CourseConfig).Assembly);


		}
	}
}
