namespace L11_Migration_Part_02_Relationships.Entities
{
	public class Enrollment
	{
		public int SectionId { get; set; }
		public int StudentId { get; set; }

		public Student Student { get; set; }
		public Section Section { get; set; }


	}


}
