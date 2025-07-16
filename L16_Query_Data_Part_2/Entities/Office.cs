namespace L16_Query_Data_Part_2.Entities
{
	public class Office
	{
		public int Id { get; set; }
		public string OfficeLocation { get; set; }
		public string OfficeName { get; set; }

		public virtual Instructor? Instructor { get; set; }

		public override string ToString()
		{
			return $"{GetType().Name}: Id={Id}, OfficeLocation={OfficeLocation}, OfficeName={OfficeName}";
		}
	}
}
