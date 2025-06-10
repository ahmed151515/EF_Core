using L12_EF_Migration_Part_03.Data.Config;
using L12_EF_Migration_Part_03.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace L12_EF_Migration_Part_03.Data
{
	public class AppDbContext : DbContext
	{

		public DbSet<Course> Courses { get; set; }
		public DbSet<Instructor> Instructors { get; set; }
		public DbSet<Office> Offices { get; set; }
		public DbSet<Section> Sections { get; set; }
		public DbSet<Schedule> Schedules { get; set; }
		public DbSet<Student> Students { get; set; }
		public DbSet<Enrollment> Enrollments { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
			var constr = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json").Build()
				.GetSection("constr").Value;

			optionsBuilder.UseSqlServer(constr);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfigurationsFromAssembly(typeof(CourseConfig).Assembly);
		}
	}

}
