using L13_Mapping_Strategies.Data.Config;
using L13_Mapping_Strategies.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace L13_Mapping_Strategies.Data
{
	public class TPHContext : DbContext
	{

		public DbSet<Course> Courses { get; set; }
		public DbSet<Instructor> Instructors { get; set; }
		public DbSet<Office> Offices { get; set; }
		public DbSet<Section> Sections { get; set; }
		public DbSet<Schedule> Schedules { get; set; }
		public DbSet<Participant> Participants { get; set; }

		public DbSet<Enrollment> Enrollments { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
			var constr = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json").Build()
				.GetSection("TPH").Value;

			optionsBuilder.UseSqlServer(constr);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfigurationsFromAssembly(typeof(CourseConfig).Assembly);

			modelBuilder.Entity<Participant>().HasDiscriminator<string>("ParticipantType")
				.HasValue<Individual>("INDV")
				.HasValue<Coporate>("COPR");

			modelBuilder.Entity<Participant>().Property("ParticipantType")
				.HasColumnType("varchar")
				.HasMaxLength(4);


		}
	}

}
