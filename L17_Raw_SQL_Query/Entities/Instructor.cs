namespace L17_Raw_SQL_Query.Entities
{
	public class Instructor
	{
		public int Id { get; set; }
		public string FName { get; set; }
		public string LName { get; set; }

		public string FullName => $"{FName} {LName}";		
		public int? OfficeId { get; set; }
		public virtual Office? Office { get; set; }

		public virtual ICollection<Section> Sections { get; set; } = new List<Section>();

        public override string ToString()
		{
			return $"{GetType().Name}: Id={Id}, FName={FName}, LName={LName}, OfficeId={OfficeId}";
		}
	}
}
