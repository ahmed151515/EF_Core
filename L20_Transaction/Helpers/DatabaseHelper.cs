using L20_Transaction.Data;
using L20_Transaction.Entities;
using Microsoft.EntityFrameworkCore;

namespace L20_Transaction.Helpers
{
	public static class DatabaseHelper
	{
		public static void RecreateCleanDatabase()
		{
			using var context = new AppDbContext();

			context.Database.EnsureDeleted();
			context.Database.EnsureCreated();
		}

		public static void PopulateDatabase()
		{
			using (var context = new AppDbContext())
			{
				context.Add(
					new BankAccount
					{
						AccountId = 1,
						AccountHolder = "Ahmed Ali",
						Balance = 10000m
					});

				context.Add(
					new BankAccount
					{
						AccountId = 2,
						AccountHolder = "Reem Ali",
						Balance = 10000m
					});

				context.SaveChanges();
			}
		}

		public static void PrintAcounts()
		{
			using var context = new AppDbContext();
			Console.WriteLine("------------------------------------");
			foreach (var item in context.BankAccounts)
			{
				Console.WriteLine($"[{item.AccountId}] {item.AccountHolder}: {item.Balance}");
			}
			Console.WriteLine("------------------------------------");
		}
		public static void PrintAcountsWithGlTransactions()
		{
			using var context = new AppDbContext();
			Console.WriteLine("------------------------------------");
			foreach (var item in context.BankAccounts.Include(e => e.GLTransactions))
			{
				Console.WriteLine($"[{item.AccountId}] {item.AccountHolder}: {item.Balance}");
				foreach (var item1 in item.GLTransactions)
				{
					Console.WriteLine($"\t[{item1.Id}] {item1.Notes} {item1.Amount} {item1.CreatedAt}");
				}
			}
			Console.WriteLine("------------------------------------");
		}
	}
}
