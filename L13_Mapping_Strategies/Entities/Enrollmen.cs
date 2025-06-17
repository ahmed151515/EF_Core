namespace L13_Mapping_Strategies.Entities
{
	public class Enrollment
	{
		public int SectionId { get; set; }
		public int StudentId { get; set; }

		public Participant Student { get; set; }
		public Section Section { get; set; }


	}


}
