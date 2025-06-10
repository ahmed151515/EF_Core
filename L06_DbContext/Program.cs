using L06_DbContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace L06_DbContext
{
	internal class Program
	{
		static void Main(string[] args)
		{
			//InternalConfiguration();
			//ExternalConfiguration();
			//UsingDependancyInjection();
			//UsingContextFactory();
			//DbContextLifeTime();
			//DbContextAndConcurrency();
			UsingContextPooling();
			Console.ReadKey();
		}

		private static void InternalConfiguration()
		{
			using var context = new Data.InternalConfigurationContext();

			foreach (var item in context.Wallets)
			{
				Console.WriteLine(item);
			}
		}

		private static void ExternalConfiguration()
		{
			var constr = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json").Build()
				.GetSection("constr").Value;
			var optionsBuilder = new DbContextOptionsBuilder();

			optionsBuilder.UseSqlServer(constr);



			using var context = new Data.ExternalConfigurationContext(optionsBuilder.Options);

			foreach (var item in context.Wallets)
			{
				Console.WriteLine(item);
			}
		}

		private static void UsingDependancyInjection()
		{
			var constr = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json").Build()
				.GetSection("constr").Value;

			var services = new ServiceCollection();

			services.AddDbContext<Data.ExternalConfigurationContext>(options => options.UseSqlServer(constr));

			IServiceProvider serviceProvider = services.BuildServiceProvider();

			using var context = serviceProvider.GetService<Data.ExternalConfigurationContext>();

			foreach (var item in context.Wallets)
			{
				Console.WriteLine(item);
			}
		}

		private static void UsingContextFactory()
		{
			// must use constructor and DbContextOptions as parameter


			var constr = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json").Build()
				.GetSection("constr").Value;

			var services = new ServiceCollection();

			services.AddDbContextFactory<Data.ExternalConfigurationContext>(options => options.UseSqlServer(constr));

			IServiceProvider serviceProvider = services.BuildServiceProvider();

			var contextFactory = serviceProvider
				.GetService<IDbContextFactory<Data.ExternalConfigurationContext>>();

			using var context = contextFactory.CreateDbContext();


			foreach (var item in context.Wallets)
			{
				Console.WriteLine(item);
			}
		}

		private static void DbContextLifeTime()
		{
			var constr = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json").Build()
				.GetSection("constr").Value;
			var optionsBuilder = new DbContextOptionsBuilder();


			optionsBuilder.UseSqlServer(constr)
			// Dbcontext Other configurations
			.LogTo(Console.WriteLine, LogLevel.Information)
				;
			// During its lifetime, the DbContext tracks all changes made to its entities.
			// Changes will only be applied to the database when SaveChanges() is called.
			// The lifetime starts when the context is created (e.g., using new, DI, or factory)
			// and ends when it is disposed (e.g., at the end of a 'using' block or scope).

			using var context = new Data.ExternalConfigurationContext(optionsBuilder.Options);

			var w1 = new Wallet { Holder = "test 16", Balance = 77 };

			context.Wallets.Add(w1);

			var w2 = new Wallet { Holder = "test 17", Balance = 77 };

			context.Wallets.Add(w2);

			context.SaveChanges();
		}

		private static void DbContextAndConcurrency()
		{
			// Issue: Using a shared DbContext instance (stored in a static field) across multiple threads in Entity Framework Core, which is not thread-safe, leads to concurrency errors like InvalidOperationException and failures in adding wallets. The solution is to use independent DbContext instances per thread via DbContextFactory or CreateScope, ensure task completion with await Task.WhenAll, and include exception handling to track errors, as successfully implemented in the final code.
			DbContextAndConcurrencyClass.main();
		}

		private static void UsingContextPooling()
		{
			var constr = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json").Build()
				.GetSection("constr").Value;

			var services = new ServiceCollection();
			// pool have max number 
			// in EF core 6.0 it is 1024
			// in before EF core 6.0 it is 128
			services.AddDbContextPool<Data.ExternalConfigurationContext>(options => options.UseSqlServer(constr));

			IServiceProvider serviceProvider = services.BuildServiceProvider();

			using var context = serviceProvider.GetService<Data.ExternalConfigurationContext>();

			foreach (var item in context.Wallets)
			{
				Console.WriteLine(item);
			}
		}

	}

	public static class DbContextAndConcurrencyClass
	{

		public static void main()
		{
			var constr = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json").Build()
				.GetSection("constr").Value;

			var services = new ServiceCollection();

			services.AddDbContextFactory<Data.ExternalConfigurationContext>(options => options.UseSqlServer(constr));

			var serviceProvider = services.BuildServiceProvider();
			var contextFactory = serviceProvider.GetService<IDbContextFactory<Data.ExternalConfigurationContext>>();

			var tasks = new[]
			{
				Task.Factory.StartNew(() => Job1(contextFactory)),
				Task.Factory.StartNew(() => Job2(contextFactory))
			};

			Task.WhenAll(tasks).ContinueWith(t =>
			{
				Console.WriteLine("Job1 & Job2 executed concurrently!");
			});
		}
		static async Task Job1(IDbContextFactory<Data.ExternalConfigurationContext> contextFactory)
		{

			using var context = contextFactory.CreateDbContext();

			try
			{

				var wallet = new Wallet { Holder = "test 24 finlay", Balance = 7 };
				context.Wallets.Add(wallet);
				await context.SaveChangesAsync();
			}
			catch (Exception e)
			{
				Console.WriteLine("-------------------------");
				Console.WriteLine("job1");
				Console.WriteLine(e);
				Console.WriteLine("-------------------------");
			}
		}

		static async Task Job2(IDbContextFactory<Data.ExternalConfigurationContext> contextFactory)
		{
			using var context = contextFactory.CreateDbContext();

			try
			{

				var wallet = new Wallet { Holder = "test 25 finlay", Balance = 7 };
				context.Wallets.Add(wallet);
				await context.SaveChangesAsync();
			}
			catch (Exception e)
			{
				Console.WriteLine("-------------------------");
				Console.WriteLine("job2");
				Console.WriteLine(e);
				Console.WriteLine("-------------------------");
			}

		}


	}


}


