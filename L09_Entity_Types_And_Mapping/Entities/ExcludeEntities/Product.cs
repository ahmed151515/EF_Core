namespace L09_Entity_Types_And_Mapping.Entities.ExcludeEntities
{

	public class Product
	{
		public Product()
		{
			Snapshot = new Snapshot();
		}

		public int Id { get; set; }
		public string Name { get; set; }
		public decimal UnitPrice { get; set; }
		public string Description { get; set; }

		public Snapshot Snapshot { get; set; }
	}
}
