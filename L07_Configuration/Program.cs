
namespace L07_Configuration
{
	internal class Program
	{
		static void Main(string[] args)
		{
			//ConfigurationByConvention();
			//OverrideConfigurationByDataAnnotations();
			//OverrideConfigurationUsingFluentAPI();
			//OverrideConfiguratiobByGroupingConfiguration();
			CallGroupingConfigurationUsingAssembly();

		}

		private static void ConfigurationByConvention()
		{
			using var context = new Data.ConventionDbContext();

			Console.WriteLine("-------- Users --------");
			foreach (var item in context.Users)
			{
				Console.WriteLine(item);
			}
			Console.WriteLine("-------- Comments --------");
			foreach (var item in context.Comments)
			{
				Console.WriteLine(item);
			}
			Console.WriteLine("-------- Tweets --------");
			foreach (var item in context.Tweets)
			{
				Console.WriteLine(item);
			}
		}

		private static void OverrideConfigurationByDataAnnotations()
		{
			using var context = new Data.DataAnnotationsDbContext();

			Console.WriteLine("-------- Users --------");
			foreach (var item in context.Users)
			{
				Console.WriteLine(item);
			}
			Console.WriteLine("-------- Comments --------");
			foreach (var item in context.Comments)
			{
				Console.WriteLine(item);
			}
			Console.WriteLine("-------- Tweets --------");
			foreach (var item in context.Tweets)
			{
				Console.WriteLine(item);
			}
		}

		private static void OverrideConfigurationUsingFluentAPI()
		{
			using var context = new Data.FluentApiDbContext();

			Console.WriteLine("-------- Users --------");
			foreach (var item in context.Users)
			{
				Console.WriteLine(item);
			}
			Console.WriteLine("-------- Comments --------");
			foreach (var item in context.Comments)
			{
				Console.WriteLine(item);
			}
			Console.WriteLine("-------- Tweets --------");
			foreach (var item in context.Tweets)
			{
				Console.WriteLine(item);
			}
		}

		private static void OverrideConfiguratiobByGroupingConfiguration()
		{
			using var context = new Data.GroupingConfigurationDbContext();

			Console.WriteLine("-------- Users --------");
			foreach (var item in context.Users)
			{
				Console.WriteLine(item);
			}
			Console.WriteLine("-------- Comments --------");
			foreach (var item in context.Comments)
			{
				Console.WriteLine(item);
			}
			Console.WriteLine("-------- Tweets --------");
			foreach (var item in context.Tweets)
			{
				Console.WriteLine(item);
			}
		}

		private static void CallGroupingConfigurationUsingAssembly()
		{
			using var context = new Data.AssemblyDbContext();

			Console.WriteLine("-------- Users --------");
			foreach (var item in context.Users)
			{
				Console.WriteLine(item);
			}
			Console.WriteLine("-------- Comments --------");
			foreach (var item in context.Comments)
			{
				Console.WriteLine(item);
			}
			Console.WriteLine("-------- Tweets --------");
			foreach (var item in context.Tweets)
			{
				Console.WriteLine(item);
			}
		}
	}
}
