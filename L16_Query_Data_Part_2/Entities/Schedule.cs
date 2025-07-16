using L16_Query_Data_Part_2.Enums;

namespace L16_Query_Data_Part_2.Entities
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

		public virtual ICollection<Section> Sections { get; set; } = new List<Section>();

		public override string ToString()
		{
			return $"{GetType().Name}: Id={Id}, Title={Title}, SUN={SUN}, MON={MON}, TUE={TUE}, WED={WED}, THU={THU}, FRI={FRI}, SAT={SAT}";
		}
	}
}
