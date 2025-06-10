using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EF_Core_JumpStart
{
	public class AppDbContext : DbContext
	{
		// Represent the collection of all entities

		public DbSet<Wallet> Wallets { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
			var constr = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json").Build()
				.GetSection("constr").Value;

			optionsBuilder.UseSqlServer(constr);
		}
	}
}
