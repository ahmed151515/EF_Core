namespace L09_Entity_Types_And_Mapping.Entities.MappingView
{
	public class Order
	{

		public int Id { get; set; }
		public string CustomerEmail { get; set; }
		public DateTime OrderDate { get; set; }
		public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

	}
}
