using System.ComponentModel.DataAnnotations.Schema;

namespace L07_Configuration.Entities.Configuration
{
	[Table("tblTweets")]
	public class Tweet
	{
		public int TweetId { get; set; }
		public int UserId { get; set; }
		public string TweetText { get; set; }
		public DateTime CreatedAt { get; set; }

		public override string? ToString()
		{
			return $"{{\n\tTweetId: {TweetId},\n\tUserId: {UserId},\n\tTweetText: {TweetText},\n\tCreatedAt: {CreatedAt}\n}}";
		}
	}

}
