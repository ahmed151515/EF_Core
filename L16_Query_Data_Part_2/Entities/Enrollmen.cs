namespace L16_Query_Data_Part_2.Entities
{
	public class Enrollment
	{
		public int SectionId { get; set; }
		public int ParticipantId { get; set; }

		public virtual Participant Student { get; set; }
		public virtual Section Section { get; set; }

		public override string ToString()
		{
			return $"{GetType().Name}: SectionId={SectionId}, ParticipantId={ParticipantId}";
		}
	}
}
