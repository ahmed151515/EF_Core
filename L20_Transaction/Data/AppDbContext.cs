using L20_Transaction.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace L20_Transaction.Data
{
	public class AppDbContext : DbContext
	{
		public DbSet<BankAccount> BankAccounts { get; set; }
		public DbSet<GLTransaction> GLTransactions { get; set; }


		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
			var constr = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json").Build()
				.GetSection("constr").Value;
			optionsBuilder          // .UseLazyLoadingProxies()
				.UseSqlServer(constr
				//,d=>
				//d.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
				//.MinBatchSize(1).MaxBatchSize(4)
				) // .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
				  //.LogTo(Console.WriteLine, LogLevel.Information)
				;


		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
		}


	}
}
