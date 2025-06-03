using L07_Configuration.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace L07_Configuration.Data.Config
{
	public class TweetConfig : IEntityTypeConfiguration<Tweet>
	{
		public void Configure(EntityTypeBuilder<Tweet> builder)
		{
			builder.ToTable("tblTweets");
		}
	}
}
