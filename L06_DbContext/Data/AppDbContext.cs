using L06_DbContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace L06_DbContext.Data
{
	public class InternalConfigurationContext : DbContext
	{


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
	public class ExternalConfigurationContext : DbContext
	{


		public DbSet<Wallet> Wallets { get; set; }

		public ExternalConfigurationContext(DbContextOptions options) : base(options)
		{

		}
	}
}
