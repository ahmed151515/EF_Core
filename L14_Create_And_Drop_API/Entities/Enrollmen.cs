namespace L14_Create_And_Drop_API.Entities
{
	public class Enrollment
	{
		public int SectionId { get; set; }
		public int ParticipantId { get; set; }

		public Participant Student { get; set; }
		public Section Section { get; set; }


	}


}
