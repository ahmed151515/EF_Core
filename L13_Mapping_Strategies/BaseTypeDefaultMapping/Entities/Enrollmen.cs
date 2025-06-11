namespace L13_Mapping_Strategies.BaseTypeDefaultMapping.Entities
{
	public class Enrollment
	{
		public int SectionId { get; set; }
		public int StudentId { get; set; }

		public Particpant Student { get; set; }
		public Section Section { get; set; }


	}


}
