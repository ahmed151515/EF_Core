using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace L02_ADO.NET
{
	internal class Program
	{
		/*
		DP: Data Providers (abstraction for DB access)

			CONN: Connection classes (used to open DB sessions)
				CONN: SqlConnection
				CONN: OracleConnection

			TX: Transaction (used to group DB operations atomically)

			CMD: Command Object (executes SQL against DB)
				CMD: SqlCommand
				CMD: OracleCommand
					PRM: Parameters (for parameterized queries)

			DR: DataReader (read-only, forward-only DB reader)
				DR: SqlDataReader
				DR: OracleDataReader

			DA: DataAdapter (bridge between DB and DataSet)
				DA: SqlDataAdapter
				DA: OracleDataAdapter

		DS: DataSet (in-memory data representation)

			DRC: DataRelationCollection (represents relationships between tables)
			DTC: DataTableCollection (collection of DataTables)
			DT: DataTable (represents one table)
				DROW: DataRow (represents one row)
				DCOL: DataColumn (represents one column)

		 XML: XML Integration (serialize/deserialize DataSet to/from XML)
		*/
		static void Main(string[] args)
		{
			var settings = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json").Build();

			//Console.WriteLine(settings.GetSection("constr").Value);
			var constr = settings.GetSection("constr").Value;

			Console.WriteLine("---------Before command--------");
			ExecuteRawSql(constr);

			//ExecuteInsertRawSql(constr);
			//ExecuteInsertExecuteScaler(constr);
			//ExecuteInsertStoredProcedure(constr);
			//ExecuteUpdateRawSql(constr);
			//ExecuteDeleteRawSql(constr);
			ExecuteTransaction(constr);

			Console.WriteLine("---------After command--------");
			ExecuteRawSql(constr);

			//ExecuteRawSqlDataAdapter(constr);

		}


		public static void ExecuteRawSql(string constr)
		{
			var conn = new SqlConnection(constr);


			var sql = "select * from wallets";

			var sqlCommand = new SqlCommand(sql, conn);

			// by default 
			sqlCommand.CommandType = CommandType.Text;

			conn.Open();

			var reader = sqlCommand.ExecuteReader();

			Wallet wallet;

			while (reader.Read())
			{
				wallet = new Wallet
				{
					Id = reader.GetInt32("Id"),
					Holder = reader.GetString("Holder"),
					Balance = reader.GetDecimal("Balance")
				};

				Console.WriteLine(wallet);
			}
			conn.Close();
		}
		public static void ExecuteInsertRawSql(string constr)
		{
			Console.Write("enter Holder:");
			var holder = Console.ReadLine();
			Console.Write("enter Balance:");
			var balance = Convert.ToDecimal(Console.ReadLine());

			var wallet = new Wallet { Holder = holder, Balance = balance };

			var conn = new SqlConnection(constr);


			//var sql = $"insert into wallets values ({wallet.Holder}, {wallet.Balance})";
			// bad practice, this is a sql injection 

			var sql = "insert into wallets values (@Holder, @Balance)";

			var holderParameter = new SqlParameter
			{
				ParameterName = "@Holder",
				SqlDbType = SqlDbType.VarChar,
				Direction = ParameterDirection.Input,
				Value = wallet.Holder
			};
			var balanceParameter = new SqlParameter
			{
				ParameterName = "@Balance",
				SqlDbType = SqlDbType.Decimal,
				Direction = ParameterDirection.Input,
				Value = wallet.Balance
			};

			var sqlCommand = new SqlCommand(sql, conn);

			sqlCommand.Parameters.Add(holderParameter);
			sqlCommand.Parameters.Add(balanceParameter);

			// by default 
			sqlCommand.CommandType = CommandType.Text;


			conn.Open();

			if (sqlCommand.ExecuteNonQuery() > 0)
			{
				Console.WriteLine("wallet is added");
			}
			else
			{
				Console.WriteLine("wallet is not added");

			}

			conn.Close();
		}
		public static void ExecuteInsertExecuteScaler(string constr)
		{
			Console.Write("enter Holder:");
			var holder = Console.ReadLine();
			Console.Write("enter Balance:");
			var balance = Convert.ToDecimal(Console.ReadLine());

			var wallet = new Wallet { Holder = holder, Balance = balance };

			var conn = new SqlConnection(constr);


			//var sql = $"insert into wallets values ({wallet.Holder}, {wallet.Balance})";
			// bad practice, this is a sql injection 

			var sql = "insert into wallets values (@Holder, @Balance);"
				+ "Select Cast(scope_identity() as int)";

			var holderParameter = new SqlParameter
			{
				ParameterName = "@Holder",
				SqlDbType = SqlDbType.VarChar,
				Direction = ParameterDirection.Input,
				Value = wallet.Holder
			};
			var balanceParameter = new SqlParameter
			{
				ParameterName = "@Balance",
				SqlDbType = SqlDbType.Decimal,
				Direction = ParameterDirection.Input,
				Value = wallet.Balance
			};

			var sqlCommand = new SqlCommand(sql, conn);

			sqlCommand.Parameters.Add(holderParameter);
			sqlCommand.Parameters.Add(balanceParameter);

			// by default 
			sqlCommand.CommandType = CommandType.Text;


			conn.Open();

			wallet.Id = (int)sqlCommand.ExecuteScalar();

			Console.WriteLine($"Id of {wallet.Holder} is [{wallet.Id}]");

			conn.Close();
		}
		public static void ExecuteInsertStoredProcedure(string constr)
		{
			//Console.Write("enter Holder:");
			//var holder = Console.ReadLine();
			//Console.Write("enter Balance:");
			//var balance = Convert.ToDecimal(Console.ReadLine());

			var wallet = new Wallet { Holder = "khaled", Balance = 5000 };

			var conn = new SqlConnection(constr);


			var holderParameter = new SqlParameter
			{
				ParameterName = "@Holder",
				SqlDbType = SqlDbType.VarChar,
				Direction = ParameterDirection.Input,
				Value = wallet.Holder
			};
			var balanceParameter = new SqlParameter
			{
				ParameterName = "@Balance",
				SqlDbType = SqlDbType.Decimal,
				Direction = ParameterDirection.Input,
				Value = wallet.Balance
			};

			var sqlCommand = new SqlCommand("addWallet", conn);

			sqlCommand.Parameters.Add(holderParameter);
			sqlCommand.Parameters.Add(balanceParameter);

			// by default 
			sqlCommand.CommandType = CommandType.StoredProcedure;


			conn.Open();

			if (sqlCommand.ExecuteNonQuery() > 0)
			{
				Console.WriteLine("wallet is added");
			}
			else
			{
				Console.WriteLine("wallet is not added");

			}

			conn.Close();
		}
		public static void ExecuteUpdateRawSql(string constr)
		{
			//Console.Write("enter Holder:");
			//var holder = Console.ReadLine();
			//Console.Write("enter Balance:");
			//var balance = Convert.ToDecimal(Console.ReadLine());

			var wallet = new Wallet { Id = 1004, Holder = "arrm", Balance = 10000 };

			var conn = new SqlConnection(constr);
			var sql = "update Wallets set Holder=@Holder,Balance=@Balance "
				+ "where id=@Id";

			var holderParameter = new SqlParameter
			{
				ParameterName = "@Holder",
				SqlDbType = SqlDbType.VarChar,
				Direction = ParameterDirection.Input,
				Value = wallet.Holder
			};
			var balanceParameter = new SqlParameter
			{
				ParameterName = "@Balance",
				SqlDbType = SqlDbType.Decimal,
				Direction = ParameterDirection.Input,
				Value = wallet.Balance
			};
			var idParameter = new SqlParameter
			{
				ParameterName = "@Id",
				SqlDbType = SqlDbType.Int,
				Direction = ParameterDirection.Input,
				Value = wallet.Id
			};

			var sqlCommand = new SqlCommand(sql, conn);

			sqlCommand.Parameters.Add(holderParameter);
			sqlCommand.Parameters.Add(balanceParameter);
			sqlCommand.Parameters.Add(idParameter);

			// by default 
			sqlCommand.CommandType = CommandType.Text;


			conn.Open();

			if (sqlCommand.ExecuteNonQuery() > 0)
			{
				Console.WriteLine("wallet is updated");
			}
			else
			{
				Console.WriteLine("wallet is not updated");

			}

			conn.Close();
		}
		public static void ExecuteDeleteRawSql(string constr)
		{
			//Console.Write("enter Holder:");
			//var holder = Console.ReadLine();
			//Console.Write("enter Balance:");
			//var balance = Convert.ToDecimal(Console.ReadLine());

			var wallet = new Wallet { Id = 2, Holder = "Reem", Balance = 5000 };

			var conn = new SqlConnection(constr);
			var sql = "delete from  Wallets "
				+ "where id=@Id";


			var idParameter = new SqlParameter
			{
				ParameterName = "@Id",
				SqlDbType = SqlDbType.Int,
				Direction = ParameterDirection.Input,
				Value = wallet.Id
			};

			var sqlCommand = new SqlCommand(sql, conn);


			sqlCommand.Parameters.Add(idParameter);

			// by default 
			sqlCommand.CommandType = CommandType.Text;


			conn.Open();

			if (sqlCommand.ExecuteNonQuery() > 0)
			{
				Console.WriteLine("wallet is deleted");
			}
			else
			{
				Console.WriteLine("wallet is not deleted");

			}

			conn.Close();
		}
		public static void ExecuteRawSqlDataAdapter(string constr)
		{
			var conn = new SqlConnection(constr);


			var sql = "select * from wallets";



			conn.Open();
			var adapter = new SqlDataAdapter(sql, conn);

			var dt = new DataTable();
			adapter.Fill(dt);
			conn.Close();

			Wallet wallet;
			foreach (DataRow r in dt.Rows)
			{
				wallet = new Wallet
				{
					Id = Convert.ToInt32(r["Id"]),
					Holder = Convert.ToString(r["Holder"]),
					Balance = Convert.ToDecimal(r["Balance"]),
				};

				Console.WriteLine(wallet);
			}
		}

		public static void ExecuteTransaction(string constr)
		{
			//Console.Write("enter Holder:");
			//var holder = Console.ReadLine();
			//Console.Write("enter Balance:");
			//var balance = Convert.ToDecimal(Console.ReadLine());


			var conn = new SqlConnection(constr);




			var sqlCommand = conn.CreateCommand();



			// by default 
			sqlCommand.CommandType = CommandType.Text;


			conn.Open();

			var transaction = conn.BeginTransaction();

			sqlCommand.Transaction = transaction;

			try
			{
				sqlCommand.CommandText = "update wallets set Balance = Balance - 1000 where id = 1";
				sqlCommand.ExecuteNonQuery();

				sqlCommand.CommandText = "update wallets set Balance = Balance + 1000 where id = 1004";
				sqlCommand.ExecuteNonQuery();

				transaction.Commit();
			}
			catch
			{
				// maybe no Connection
				try
				{
					transaction.Rollback();
				}
				catch
				{
					// logs error
				}
			}
			finally
			{
				// maybe no Connection
				try
				{
					conn.Close();
				}
				catch
				{
					// logs error
				}
			}


		}

	}
}
