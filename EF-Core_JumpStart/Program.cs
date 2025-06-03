using Model;

namespace EF_Core_JumpStart
{
	internal class Program
	{
		static void Main(string[] args)
		{


			RetrieveData();
			//RetrieveDataSingleItem();
			//QueryData();



			//InsertWallet();
			//UpdateWallet();
			//DeleteWallet();

			//ImplementTransactions();


		}



		private static void RetrieveData()
		{
			using var context = new AppDbContext();

			var wallets = context.Wallets;

			foreach (var item in wallets)
			{
				Console.WriteLine(item);
			}
		}
		private static void RetrieveDataSingleItem()
		{
			using var context = new AppDbContext();

			var wallet = context.Wallets.FirstOrDefault(w => w.Id == 2009);

			Console.WriteLine(wallet);
		}

		private static void InsertWallet()
		{
			var wallet = new Wallet { Holder = "test 13", Balance = 51 };

			using var context = new AppDbContext();


			Console.WriteLine("------------Before DML-------------");
			RetrieveData();

			// on memory 
			context.Wallets.Add(wallet);

			context.SaveChanges();
			// id added after SaveChanges
			Console.WriteLine("---------");
			Console.WriteLine(wallet);
			Console.WriteLine("---------");
			Console.WriteLine("------------Aftere DML-------------");
			RetrieveData();

		}

		private static void UpdateWallet()
		{
			using var context = new AppDbContext();
			Console.WriteLine("------------Before DML-------------");
			RetrieveData();

			var wallet = context.Wallets.FirstOrDefault(w => w.Id == 2011);
			// any changes EF track it 
			wallet.Balance += 49;

			// Persists all tracked changes to the database
			//  unit of work
			context.SaveChanges();
			Console.WriteLine("------------Aftere DML-------------");
			RetrieveData();

		}

		private static void DeleteWallet()
		{

			using var context = new AppDbContext();


			Console.WriteLine("------------Before DML-------------");
			RetrieveData();
			var wallet = context.Wallets.FirstOrDefault(w => w.Id == 2012);

			context.Wallets.Remove(wallet);

			context.SaveChanges();


			Console.WriteLine("------------Aftere DML-------------");
			RetrieveData();
		}
		private static void QueryData()
		{
			using var context = new AppDbContext();

			var walletBalanceAbove5000 = context.Wallets.Where(w => w.Balance > 5000);

			foreach (var item in walletBalanceAbove5000)
			{
				Console.WriteLine(item);
			}
		}

		private static void ImplementTransactions()
		{
			var amount = 30_000m;
			using var context = new AppDbContext();
			Console.WriteLine("------------Before Transaction-------------");
			RetrieveData();
			using (var tran = context.Database.BeginTransaction())
			{
				var from = context.Wallets.FirstOrDefault(w => w.Id == 1003);
				var to = context.Wallets.FirstOrDefault(w => w.Id == 1);

				if (from.Balance < amount)
				{
					throw new InvalidOperationException();
				}

				from.Balance -= amount;
				to.Balance += amount;

				context.SaveChanges();
				tran.Commit();

			}
			Console.WriteLine("------------Aftere Transaction-------------");
			RetrieveData();
		}

	}
}
