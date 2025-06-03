using System.ComponentModel.DataAnnotations.Schema;

namespace L07_Configuration.Entities.Configuration
{
	[Table("tblUsers")]
	public class User
	{

		public int UserId { get; set; }
		[Column("Username")]
		public string name { get; set; }

		public override string? ToString()
		{
			return $"{{\n\tUserId: {UserId},\n\tname: {name}\n}}";
		}
	}

}
