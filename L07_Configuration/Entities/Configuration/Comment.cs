using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace L07_Configuration.Entities.Configuration
{
	[Table("tblComments")]
	public class Comment
	{
		[Column("CommentId")]
		public int Id { get; set; }
		public int TweetId { get; set; }
		public int UserId { get; set; }
		public string CommentText { get; set; }
		public DateTime CreatedAt { get; set; }

		public override string? ToString()
		{
			return $"{{\n\tId: {Id},\n\tTweetId: {TweetId},\n\tUserId: {UserId},\n\tCommentText: {CommentText},\n\tCreatedAt: {CreatedAt}\n}}";
		}
	}

}
