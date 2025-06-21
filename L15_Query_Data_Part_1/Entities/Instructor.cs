namespace L15_Query_Data_Part_1.Entities
{
	public class Instructor
	{
		public int Id { get; set; }
		public string FName { get; set; }
		public string LName { get; set; }

		public int? OfficeId { get; set; }
		public virtual Office? Office { get; set; }

		public virtual ICollection<Section> Sections { get; set; } = new List<Section>();

		public override string ToString()
		{
			return $"{GetType().Name}: Id={Id}, FName={FName}, LName={LName}, OfficeId={OfficeId}";
		}
	}
}
