using L13_Mapping_Strategies.BaseTypeDefaultMapping.Data.Config;
using L13_Mapping_Strategies.BaseTypeDefaultMapping.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace L13_Mapping_Strategies.BaseTypeDefaultMapping.Data
{
	public class DefaultContext : DbContext
	{

		public DbSet<Course> Courses { get; set; }
		public DbSet<Instructor> Instructors { get; set; }
		public DbSet<Office> Offices { get; set; }
		public DbSet<Section> Sections { get; set; }
		public DbSet<Schedule> Schedules { get; set; }
		public DbSet<Particpant> Particpants { get; set; }
		public DbSet<Enrollment> Enrollments { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
			var constr = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json").Build()
				.GetSection("BaseTypeDefaultMapping").Value;

			optionsBuilder.UseSqlServer(constr);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfigurationsFromAssembly(typeof(CourseConfig).Assembly);
		}
	}

}
