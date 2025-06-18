namespace L14_Create_And_Drop_API.Entities
{
	public class Participant
	{
		public int Id { get; set; }

		public string? FName { get; set; }

		public string? LName { get; set; }


		public ICollection<Section> Sections { get; set; } = new List<Section>();
		public override string ToString()
		{
			return $"{GetType().Name}: Id={Id}, FName={FName}, LName={LName}";
		}




	}


}
