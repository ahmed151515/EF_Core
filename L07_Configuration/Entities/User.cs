namespace L07_Configuration.Entities
{
	public class User
	{


		// Primary key naming conventions: [Id, id, ID], or [ClassNameId]
		// These conventions apply when using Code-First approach.
		// In DB-First, the property name must match the actual column name in the database schema.

		public int UserId { get; set; }
		public string name { get; set; }

		public override string? ToString()
		{
			return $"{{\n\tUserId: {UserId},\n\tname: {name}\n}}";
		}
	}


}
