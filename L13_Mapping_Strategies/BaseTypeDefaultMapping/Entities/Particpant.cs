namespace L13_Mapping_Strategies.BaseTypeDefaultMapping.Entities
{
	public class Particpant
	{
		public int Id { get; set; }

		public string? FName { get; set; }

		public string? LName { get; set; }


		public ICollection<Section> Sections { get; set; } = new List<Section>();

	}

	public class Individual
	{

	}


}
