namespace L14_Create_And_Drop_API.Entities
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
