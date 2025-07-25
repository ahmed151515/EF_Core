using L18_Save_Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace L18_Save_Data.Data
{
	public class AppDbContext : DbContext
	{
		public DbSet<Author> Authors { get; set; }
		public DbSet<Book> Books { get; set; }
		public DbSet<AuthorV2> AuthorV2s { get; set; }
		public DbSet<BookV2> BookV2s { get; set; }

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
