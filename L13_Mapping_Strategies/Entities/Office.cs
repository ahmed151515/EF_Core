﻿namespace L13_Mapping_Strategies.Entities
{
	public class Office
	{
		public int Id { get; set; }
		public string OfficeLocation { get; set; }
		public string OfficeName { get; set; }

		public Instructor? Instructor { get; set; }

	}
}
