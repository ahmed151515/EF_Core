namespace L17_Raw_SQL_Query.Entities
{
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


}
