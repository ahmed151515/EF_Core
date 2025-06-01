using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using model;
using System.Data;
using System.Transactions;


namespace L03_Dapper_orm
{
	internal class Program
	{

		static void Main(string[] args)
		{
			var settings = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json").Build();

			//Console.WriteLine(settings.GetSection("constr").Value);
			var constr = settings.GetSection("constr").Value;
			//Console.WriteLine("---------Before command--------");

			ExecuteRawSql(constr);

			//ExecuteInsertRawSql(constr);
			//ExecuteInsertReturnIdentity(constr);
			//ExecuteUpdateRawSql(constr);
			//ExecuteDeleteRawSql(constr);
			//DapperAndTransactions(constr);

			//Console.WriteLine("---------After command--------");
			//ExecuteRawSql(constr);

			//ExecuteMultipleQueries(constr);

		}


		public static void ExecuteRawSql(string constr)
		{
			IDbConnection db = new SqlConnection(constr);


			var sql = "select * from wallets";

			//Console.WriteLine("------------using Dynamic query-----------");
			//var resultAsDynamic = db.Query(sql);

			//foreach (var item in resultAsDynamic)
			//{
			//	Console.WriteLine(item);
			//}

			//Console.WriteLine("------------using Typed query-----------");

			var resultAsTyped = db.Query<Wallet>(sql);

			foreach (var item in resultAsTyped)
			{
				Console.WriteLine(item);
			}

		}
		public static void ExecuteInsertRawSql(string constr)
		{


			var wallet = new Wallet { Holder = "salah 2", Balance = 10_000m };

			IDbConnection db = new SqlConnection(constr);


			//var sql = $"insert into wallets values ({wallet.Holder}, {wallet.Balance})";
			// bad practice, this is a sql injection 

			var sql = "insert into wallets values (@Holder, @Balance)";

			db.Execute(sql, new { Holder = wallet.Holder, Balance = wallet.Balance });


		}
		public static void ExecuteInsertReturnIdentity(string constr)
		{


			var wallet = new Wallet { Holder = "test 4", Balance = 10_000m };

			IDbConnection db = new SqlConnection(constr);


			//var sql = $"insert into wallets values ({wallet.Holder}, {wallet.Balance})";
			// bad practice, this is a sql injection 

			var sql = "insert into wallets values (@Holder, @Balance);"
				+ "Select Cast(scope_identity() as int)";

			// my guess
			//int id = (int)db.ExecuteScalar(sql, new
			//{
			//	Holder = wallet.Holder,
			//	Balance = wallet.Balance
			//});

			int id = db.Query<int>(sql, new
			{
				Holder = wallet.Holder,
				Balance = wallet.Balance
			}).Single();

			wallet.Id = id;

			Console.WriteLine(wallet);
		}
		public static void ExecuteUpdateRawSql(string constr)
		{


			var wallet = new Wallet { Id = 1008, Holder = "test 3 updated", Balance = 500 };

			IDbConnection db = new SqlConnection(constr);
			var sql = "update Wallets set Holder=@Holder,Balance=@Balance "
				+ "where id=@Id";

			db.Execute(sql, new
			{
				Holder = wallet.Holder,
				Balance = wallet.Balance,
				Id = wallet.Id
			});


		}
		public static void ExecuteDeleteRawSql(string constr)
		{
			//Console.Write("enter Holder:");
			//var holder = Console.ReadLine();
			//Console.Write("enter Balance:");
			//var balance = Convert.ToDecimal(Console.ReadLine());

			var wallet = new Wallet { Id = 1008, Holder = "test 3 updated", Balance = 500 };

			IDbConnection db = new SqlConnection(constr);
			var sql = "delete from  Wallets "
				+ "where id=@Id";

			db.Execute(sql, new
			{
				Id = wallet.Id
			});

		}
		public static void ExecuteMultipleQueries(string constr)
		{
			IDbConnection db = new SqlConnection(constr);


			var sql = "select min(balance) from wallets;"
				+ "select max(balance) from wallets;";

			var res = db.QueryMultiple(sql);

			// Each Read<T>() moves forward to the next result set.
			// Must read in the same order as the queries were written.
			Console.WriteLine($"Min: {res.ReadSingle<decimal>()}");
			Console.WriteLine($"Max: {res.ReadSingle<decimal>()}");
		}

		public static void DapperAndTransactions(string constr)
		{
			IDbConnection db = new SqlConnection(constr);



			// instructor
			// Transfer 2000
			// From: 1004   Sarah   10000
			// To:  1   Menna   5500

			decimal amountToTranfer = 2000m;



			using (var tran = new TransactionScope())
			{
				var from = db.QueryFirst<Wallet>("select *  from wallets where id=@id", new
				{
					id = 1004
				});
				var to = db.QueryFirst<Wallet>("select *  from wallets where id=@id", new
				{
					id = 1
				});
				if (from.Balance < amountToTranfer) throw new InvalidOperationException("Not has money");
				from.Balance -= amountToTranfer;
				to.Balance += amountToTranfer;

				db.Execute("update Wallets set Balance = @Balance where id=@Id", new
				{
					Balance = from.Balance,
					Id = from.Id
				});

				db.Execute("update Wallets set Balance = @Balance where id=@Id", new
				{
					Balance = to.Balance,
					Id = to.Id
				});

				tran.Complete();
			}




		}

	}
}
