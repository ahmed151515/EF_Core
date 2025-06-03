using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Configuration = L07_Configuration.Entities.Configuration;

namespace L07_Configuration.Data
{
	public class DataAnnotationsDbContext : DbContext
	{
		public DbSet<Configuration.User> Users { get; set; }
		public DbSet<Configuration.Comment> Comments { get; set; }
		public DbSet<Configuration.Tweet> Tweets { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);

			var v1ConnectionString = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json").Build()
				.GetSection("v1").Value;

			optionsBuilder.UseSqlServer(v1ConnectionString);
		}

	}

}
