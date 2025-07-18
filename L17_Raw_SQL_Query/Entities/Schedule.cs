using L17_Raw_SQL_Query.Enums;

namespace L17_Raw_SQL_Query.Entities
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
