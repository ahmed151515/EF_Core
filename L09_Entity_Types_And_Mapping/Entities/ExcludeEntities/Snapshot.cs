namespace L09_Entity_Types_And_Mapping.Entities.ExcludeEntities
{
	//[NotMapped]
	public class Snapshot
	{
		public DateTime LoadedAt => DateTime.UtcNow;

		public string Version => Guid.NewGuid().ToString().Substring(0, 8);

		public override string ToString()
		{
			return $"LoadedAt: {LoadedAt} - Version: {Version}";
		}
	}

}
