using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Convention = L07_Configuration.Entities.Convention;

namespace L07_Configuration.Data
{


	public class ConventionDbContext : DbContext
	{
		// name of Property must same name of table in DB
		public DbSet<Convention.User> Users { get; set; }
		public DbSet<Convention.Comment> Comments { get; set; }
		public DbSet<Convention.Tweet> Tweets { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);

			var v2ConnectionString = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json").Build()
				.GetSection("v2").Value;

			optionsBuilder.UseSqlServer(v2ConnectionString);
		}

	}

}
