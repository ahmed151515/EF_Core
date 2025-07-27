
using L20_Transaction.Data;
using L20_Transaction.Entities;
using L20_Transaction.Helpers;

namespace L20_Transaction
{
	internal class Program
	{
		static Random _random = new Random();

		static void Main(string[] args)
		{
			//Run_Initial_Transfer_WalkThrough();

			//Run_Changes_Within_Multiple_SaveChanges();

			//Run_Changes_Within_Single_SaveChanges();

			//Run_Changes_Within_Mutlitple_SaveChanges_In_DbTransaction();

			//Run_Changes_Within_Multiple_SaveChanges_In_DbTransaction_Best_Practice();

			//Run_ChangesDbTransaction_SavePoints();
			MyTry();
		}



		private static void Run_Initial_Transfer_WalkThrough()
		{
			var a1 = new BankAccount
			{
				AccountId = 1,
				AccountHolder = "test 1",
				Balance = 1000m
			};

			var a2 = new BankAccount
			{
				AccountId = 2,
				AccountHolder = "test 2",
				Balance = 1000m
			};

			a1.Withdraw(100);

			a2.Deposit(100);

			Console.ReadKey();

			// the problem if Deposit is failed what happen for 100 from a1
		}

		private static void Run_Changes_Within_Multiple_SaveChanges()
		{
			DatabaseHelper.RecreateCleanDatabase();
			DatabaseHelper.PopulateDatabase();

			using var context = new AppDbContext();

			var a1 = context.BankAccounts.First(a => a.AccountId == 1);
			var a2 = context.BankAccounts.First(a => a.AccountId == 2);

			decimal amount = 100;
			try
			{


				a1.Withdraw(amount);

				context.SaveChanges();

				if (_random.Next(0, 1) == 0)
				{
					throw new Exception();
				}

				a2.Deposit(amount);

				context.SaveChanges();
			}
			catch (Exception e)
			{
				Console.WriteLine("Error in Transfer");
			}
			finally
			{
				DatabaseHelper.PrintAcountsWithGlTransactions();
			}

		}

		private static void Run_Changes_Within_Single_SaveChanges()
		{
			DatabaseHelper.RecreateCleanDatabase();
			DatabaseHelper.PopulateDatabase();

			using var context = new AppDbContext();

			var a1 = context.BankAccounts.First(a => a.AccountId == 1);
			var a2 = context.BankAccounts.First(a => a.AccountId == 2);

			decimal amount = 100;
			try
			{


				a1.Withdraw(amount);

				if (_random.Next(0, 2) == 0)
				{
					throw new Exception();
				}

				a2.Deposit(amount);

				context.SaveChanges();
			}
			catch (Exception e)
			{
				Console.WriteLine("Error in Transfer");
			}
			finally
			{
				DatabaseHelper.PrintAcountsWithGlTransactions();
			}
		}

		private static void Run_Changes_Within_Mutlitple_SaveChanges_In_DbTransaction()
		{
			DatabaseHelper.RecreateCleanDatabase();
			DatabaseHelper.PopulateDatabase();

			using var context = new AppDbContext();
			using (var tran = context.Database.BeginTransaction())
			{

				var a1 = context.BankAccounts.First(a => a.AccountId == 1);
				var a2 = context.BankAccounts.First(a => a.AccountId == 2);

				decimal amount = 100;
				try
				{


					a1.Withdraw(amount);

					context.SaveChanges();

					if (_random.Next(0, 2) == 0)
					{
						throw new Exception();
					}

					a2.Deposit(amount);

					context.SaveChanges();

					tran.Commit();
				}
				catch (Exception e)
				{
					Console.WriteLine("Error in Transfer ");
				}
				finally
				{
					DatabaseHelper.PrintAcountsWithGlTransactions();
				}

			}
		}
		private static void Run_Changes_Within_Multiple_SaveChanges_In_DbTransaction_Best_Practice()
		{
			DatabaseHelper.RecreateCleanDatabase();
			DatabaseHelper.PopulateDatabase();

			using var context = new AppDbContext();
			using (var tran = context.Database.BeginTransaction())
			{

				var a1 = context.BankAccounts.First(a => a.AccountId == 1);
				var a2 = context.BankAccounts.First(a => a.AccountId == 2);

				decimal amount = 100;
				try
				{


					a1.Withdraw(amount);

					context.SaveChanges();

					if (_random.Next(0, 2) == 0)
					{
						throw new Exception();
					}

					a2.Deposit(amount);

					context.SaveChanges();

					tran.Commit();
				}
				catch (Exception e)
				{
					tran.Rollback();
					Console.WriteLine("Error in Transfer ");
				}
				finally
				{
					DatabaseHelper.PrintAcountsWithGlTransactions();
				}
			}


		}
		private static void Run_ChangesDbTransaction_SavePoints()
		{
			DatabaseHelper.RecreateCleanDatabase();
			DatabaseHelper.PopulateDatabase();

			using var context = new AppDbContext();
			using (var tran = context.Database.BeginTransaction())
			{
				try
				{
					var a1 = context.BankAccounts.First(a => a.AccountId == 1);
					var a2 = context.BankAccounts.First(a => a.AccountId == 2);

					decimal amount = 100;

					tran.CreateSavepoint("read_acount");





					a1.Withdraw(amount);

					context.SaveChanges();



					if (_random.Next(0, 2) == 0)
					{
						throw new Exception();
					}

					a2.Deposit(amount);

					context.SaveChanges();


					tran.Commit();

				}
				catch (Exception e)
				{
					tran.RollbackToSavepoint("read_acount");
					Console.WriteLine("Error in Transfer ");

				}
				finally
				{
					DatabaseHelper.PrintAcountsWithGlTransactions();
				}
			}
		}
		private static void MyTry()
		{
			DatabaseHelper.RecreateCleanDatabase();
			DatabaseHelper.PopulateDatabase();

			using var context = new AppDbContext();

			decimal amount = 100;

			try
			{

				using (var tran = context.Database.BeginTransaction())
				{
					var a1 = context.BankAccounts.First(a => a.AccountId == 1);
					var a2 = context.BankAccounts.First(a => a.AccountId == 2);

					tran.CreateSavepoint("read_data");

					for (int i = 1; i <= 3; i++)
					{
						try
						{

							if (i != 1)
							{
								context.Entry(a1).Reload();
								context.Entry(a2).Reload();
							}

							a1.Withdraw(amount);

							context.SaveChanges();


							if (_random.Next(0, 2) == 0)
							{
								throw new Exception();
							}

							a2.Deposit(amount);

							context.SaveChanges();

							tran.Commit();
							break;
						}
						catch (Exception e)
						{
							Console.WriteLine($"try {i} is Failure");
							tran.RollbackToSavepoint("read_data");
							if (i == 3)
							{
								throw new Exception();
							}

						}

					}
				}
			}
			catch
			{
				Console.WriteLine("the Transfer is Failure");
			}
			finally
			{
				DatabaseHelper.PrintAcountsWithGlTransactions();

			}

		}
	}
}
