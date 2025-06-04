using L09_Entity_Types_And_Mapping.Entities.TableValuedFunction;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace L09_Entity_Types_And_Mapping.Data;
public class TableValuedFunctionContext : DbContext
{
	public DbSet<Product> Products { get; set; }
	public DbSet<Order> Orders { get; set; }
	public DbSet<OrderDetail> OrderDetails { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		base.OnConfiguring(optionsBuilder);

		var constr = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build()
			.GetSection("constr").Value;

		optionsBuilder.UseSqlServer(constr);
	}
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<Order>().ToTable("Orders", "Sales");


		modelBuilder.Entity<OrderDetail>().ToTable("OrderDetails", "Sales");


		modelBuilder.Entity<Product>().ToTable("Products", "Inventory");

		modelBuilder.Entity<OrderBill>().ToFunction("GetOrderBill").HasNoKey();



	}
}

