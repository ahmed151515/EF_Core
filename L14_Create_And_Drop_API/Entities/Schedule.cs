﻿using L14_Create_And_Drop_API.Enums;

namespace L14_Create_And_Drop_API.Entities
{
	public class Schedule
	{
		public int Id { get; set; }
		public ScheduleEnum Title { get; set; }
		public bool SUN { get; set; }
		public bool MON { get; set; }
		public bool TUE { get; set; }
		public bool WED { get; set; }
		public bool THU { get; set; }
		public bool FRI { get; set; }
		public bool SAT { get; set; }

		public ICollection<Section> Sections { get; set; } = new List<Section>();

	}


}
