
using L09_Entity_Types_And_Mapping.Data;
using L09_Entity_Types_And_Mapping.Entities.IncludeEntities;
using L09_Entity_Types_And_Mapping.Entities.TableValuedFunction;
using Microsoft.EntityFrameworkCore;

namespace L09_Entity_Types_And_Mapping
{
	internal class Program
	{
		static void Main(string[] args)
		{
			//BasicSetup();
			//ExcludeEntities();
			//IncludeEntities();
			//MappingView();
			TableValuedFunction();

		}

		private static void BasicSetup()
		{
			using var context = new BasicSetupContext();
			Console.WriteLine("-------------- Products --------------");
			foreach (var item in context.Products)
			{
				Console.WriteLine(item.Name);
				Console.WriteLine(item.UnitPrice);
			}
			Console.WriteLine("-------------- Orders --------------");
			foreach (var item in context.Orders)
			{
				Console.WriteLine(item.CustomerEmail);
			}
			Console.WriteLine("-------------- OrderDetails --------------");
			foreach (var item in context.OrderDetails)
			{

				Console.WriteLine(item.OrderId);
			}
		}

		private static void ExcludeEntities()
		{
			using var context = new ExcludeEntitiesContext();

			Console.WriteLine("-------------- Products --------------");
			foreach (var item in context.Products)
			{
				Console.WriteLine(item.Name);
				Console.WriteLine(item.Snapshot);
			}
		}

		private static void IncludeEntities()
		{
			using var context = new IncludeEntitiesContext();

			//var itemsInOrder1 = context.OrderDetails.Where(o => o.OrderId == 1); // compile time erorr



			var order1 = context.Orders.Include(o => o.OrderDetails)
				.FirstOrDefault(o => o.Id == 1);
			var itemsInOrder1 = order1.OrderDetails;
			Console.WriteLine($"items count in order 1: {itemsInOrder1.Count()}");

			//var audit = new AuditEntry { UserName = "ahmed", Action = "Destroy database" };

			//context.Set<AuditEntry>().Add(audit);

			//context.SaveChanges();

			Console.WriteLine("--------- Audit Logs ----------");
			foreach (var item in context.Set<AuditEntry>())
			{
				Console.WriteLine(item);
			}
		}

		private static void MappingView()
		{
			using var context = new MappingViewContext();

			foreach (var item in context.OrderWithDetailsView)
			{
				Console.WriteLine(item);
			}
		}

		private static void TableValuedFunction()
		{
			using var context = new TableValuedFunctionContext();
			var orderBill1 = context.Set<OrderBill>().FromSqlInterpolated($"select * from GetOrderBill({1})");
			foreach (var item in orderBill1)
			{
				Console.WriteLine(item);
			}
		}
	}
}
