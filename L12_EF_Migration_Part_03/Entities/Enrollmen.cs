namespace L12_EF_Migration_Part_03.Entities
{
	public class Enrollment
	{
		public int SectionId { get; set; }
		public int StudentId { get; set; }

		public Student Student { get; set; }
		public Section Section { get; set; }


	}


}
