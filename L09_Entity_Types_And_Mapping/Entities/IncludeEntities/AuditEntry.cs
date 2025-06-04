namespace L09_Entity_Types_And_Mapping.Entities.IncludeEntities
{
	public class AuditEntry
	{
		public int Id { get; set; }
		public string UserName { get; set; }
		public string Action { get; set; }

		public override string ToString()
		{
			return $"Id: {Id}, UserName: {UserName}, Action: {Action}";
		}
	}
}
