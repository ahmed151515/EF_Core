namespace L16_Query_Data_Part_2.Entities
{
	public class Participant
	{
		public int Id { get; set; }

		public string? FName { get; set; }

		public string? LName { get; set; }

        public string FullName => $"{FName} {LName}";
        public virtual ICollection<Section> Sections { get; set; } = new List<Section>();
		public override string ToString()
		{
			return $"{GetType().Name}: Id={Id}, FName={FName}, LName={LName}";
		}




	}


}
