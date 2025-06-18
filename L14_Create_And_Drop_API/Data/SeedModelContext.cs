using L14_Create_And_Drop_API.Data.Config;
using L14_Create_And_Drop_API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace L14_Create_And_Drop_API.Data
{
	public class SeedModelContext : DbContext
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
				.GetSection("SeedModel").Value;

			optionsBuilder.UseSqlServer(constr);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfigurationsFromAssembly(typeof(CourseConfig).Assembly);

			modelBuilder.Entity<Coporate>().HasData(SeedData.LoadCoporates());
			modelBuilder.Entity<Individual>().HasData(SeedData.LoadIndividuals());
			modelBuilder.Entity<Course>().HasData(SeedData.LoadCourses());
			modelBuilder.Entity<Instructor>().HasData(SeedData.LoadInstructors());
			modelBuilder.Entity<Office>().HasData(SeedData.LoadOffices());
			modelBuilder.Entity<Schedule>().HasData(SeedData.LoadSchedules());
			modelBuilder.Entity<Enrollment>().HasData(SeedData.LoadEnrollments());
		}
	}
}
