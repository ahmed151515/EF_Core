namespace L17_Raw_SQL_Query.Entities
{
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
