using L19_Interceptors.Data.Interceptors;
using L19_Interceptors.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace L19_Interceptors.Data
{
	public class AppDbContext : DbContext
	{
		public DbSet<Book> Books { get; set; }


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
				  .AddInterceptors(new SoftDeleteInterceptor())
				;


		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
		}


	}
}
