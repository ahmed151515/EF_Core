using CLI = L08_Reverse_Engineering.Data_cli;
using PMC = L08_Reverse_Engineering.Data;

namespace L08_Reverse_Engineering
{
	internal class Program
	{
		/*
		 * How to keep EF-Core Model & DB Schema in sync?
		 *	Code source of truth
		 *		migrations 
		 *		Greenfield Projects
		 *	DB source of truth
		 *		Reverse Engineering
		 *			old projects
		 *			DB very big
		 *		Limitations
		 *			Providers not equal
		 *			Data Type support/ Provider
		 *			Concurrency Token in EF-core issue
		 *		Pre-Requisite
		 *			PMC tool / dotnet CLI tool
		 *			Microsoft.EntityFrameworkCore.Design
		 *			Install Provider for Database Schema
		*/


		static void Main(string[] args)
		{
			// Step #1: Package Manager Console (PMC)
			//    Tools -> Nuget Package Manager -> Package Manager Console

			// Step #2: Package Manager Console (PMC) Tool 
			//    Install-Package Microsoft.EntityFrameworkCore.Tools

			// Step #3: Install Nuget Page on Project Microsoft.EntityFrameworkCore.Design
			// Microsoft.EntityFrameworkCore.SqlServer

			// Step #4: Install Provider in the project Microsoft.EntityFrameworkCore.SqlServer

			// Step #5: Run Command (Full)
			//    Scaffold-DbContext '[Connection String]' [Provider]

			//  Scaffold-DbContext 'Server=.;Database=TechTalk;User=sa;Password=SqlServer1;TrustServerCertificate=True' Microsoft.EntityFrameworkCore.SqlServer -Context AppDbContext -ContextDir Data -OutputDir Entities
			ReverseEngineering();

			// Step #1: Windows Terminal (Command Prompt) 

			// Step #2: Install Ef-Core tool globally
			//    dotnet tool install --global dotnet-ef    (new)
			//    dotnet tool update --global dotnet-ef     (to upgrade)

			// Step #3: Install Provider in the project Microsoft.EntityFrameworkCore.SqlServer

			// Step #4: Run Command (Full)
			//    dotnet ef dbcontext scaffold '[Connection String]' [Provider]
			// dotnet ef dbcontext scaffold 'Server=.;Database=TechTalk;User=sa;Password=SqlServer1;TrustServerCertificate=True' Microsoft.EntityFrameworkCore.SqlServer -c AppDbContext --Context-Dir Data -o Entities
			ReverseEngineeringNETCLI();


		}



		private static void ReverseEngineering()
		{
			using var context = new PMC.AppDbContext();
			foreach (var item in context.Speakers)
			{

				Console.WriteLine(item.FirstName + " " + item.LastName);

			}
		}

		private static void ReverseEngineeringNETCLI()
		{
			using var context = new CLI.AppDbContext();
			foreach (var item in context.Speakers)
			{

				Console.WriteLine(item.FirstName + " " + item.LastName);

			}
		}


	}
}
