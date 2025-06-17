namespace L13_Mapping_Strategies.Entities
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

	public class Individual : Participant
	{
		public string University { get; set; }
		public int YearOfGraduation { get; set; }
		public bool IsIntern { get; set; }
		public override string ToString()
		{
			return $"{base.ToString()}, University={University}, YearOfGraduation={YearOfGraduation}, IsIntern={IsIntern}";
		}
	}
	public class Coporate : Participant
	{
		public string Company { get; set; }
		public string JobTitle { get; set; }
		public override string ToString()
		{
			return $"{base.ToString()}, Company={Company}, JobTitle={JobTitle}";
		}
	}


}
