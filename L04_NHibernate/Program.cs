using Microsoft.Extensions.Configuration;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;

namespace L04_NHibernate
{
	internal class Program
	{
		private static ISession CreateSession(bool printQuery = false)
		{
			var congifg = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

			var ConStr = congifg.GetSection("constr").Value;


			var mapper = new ModelMapper();


			// list all of type mappings from assembly
			mapper.AddMappings(typeof(Wallet).Assembly.ExportedTypes);


			// Compile class mapping
			HbmMapping domainMapping = mapper.CompileMappingForAllExplicitlyAddedEntities();


			//Console.WriteLine(domainMapping.AsString());


			// allow the application to specify propertties and mapping documents
			// to be used when creating

			var hbConfig = new Configuration();

			// settings from app to nhibernate 
			hbConfig.DataBaseIntegration(c =>
			{
				// strategy to interact with provider
				c.Driver<MicrosoftDataSqlClientDriver>();
				// dialect nhibernate uses to build syntaxt to rdbms
				c.Dialect<MsSql2012Dialect>();
				// connection string
				c.ConnectionString = ConStr;


				// log sql statement to console (optional)
				c.LogSqlInConsole = printQuery;
				// format logged sql statement (optional)
				c.LogFormattedSql = printQuery;
			});
			// add mapping to nhiberate configuration
			hbConfig.AddMapping(domainMapping);

			// instantiate a new IsessionFactory (use properties, settings and mapping)

			var sessionFactory = hbConfig.BuildSessionFactory();

			var session = sessionFactory.OpenSession();

			return session;
		}

		static void Main(string[] args)
		{
			using (var session = CreateSession(printQuery: false))
			{
				//   NHibernate recommends wrapping all database access logic in transactions
				using (var tran = session.BeginTransaction())
				{

					//Console.WriteLine(session.IsConnected);
					//RetrieveData(session);
					//RetrieveWalletById(session);
					Console.WriteLine("------------Before DML-------------");
					RetrieveData(session);

					//InsertWallet(session);
					//UpdateWallet(session);
					//DeleteWallet(session);
					TransferFunds(session);

					Console.WriteLine("------------Aftere DML-------------");
					RetrieveData(session);

					tran.Commit();
				}


			}
		}

		private static void RetrieveData(ISession session)
		{

			var wallets = session.Query<Wallet>();

			foreach (var item in wallets)
			{
				Console.WriteLine(item);
			}
		}

		private static void RetrieveWalletById(ISession session)
		{

			var wallet = session.Query<Wallet>().FirstOrDefault(w => w.Id == 1);
			Console.WriteLine(wallet);

			var wallet_2 = session.Get<Wallet>(1);
			Console.WriteLine(wallet_2);

		}

		private static void InsertWallet(ISession session)
		{
			var wallet = new Wallet { Holder = "test 9", Balance = 40 };

			session.Save(wallet);
			Console.WriteLine("------");
			Console.WriteLine(wallet);
			Console.WriteLine("------");
		}

		private static void UpdateWallet(ISession session)
		{
			var wallet = session.Get<Wallet>(2009);

			wallet.Balance = 1000;

			session.Update(wallet);
		}

		private static void DeleteWallet(ISession session)
		{
			var wallet = session.Get<Wallet>(2008);

			session.Delete(wallet);


		}

		private static void TransferFunds(ISession session)
		{
			var amount = 1000m;

			var from = session.Get<Wallet>(1);
			var to = session.Get<Wallet>(2007);

			from.Balance -= amount;
			to.Balance += amount;

			session.Update(from);
			session.Update(to);
		}

	}
}
