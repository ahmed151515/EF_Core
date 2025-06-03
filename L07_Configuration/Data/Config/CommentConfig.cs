using L07_Configuration.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace L07_Configuration.Data.Config
{
	public class CommentConfig : IEntityTypeConfiguration<Comment>
	{


		public void Configure(EntityTypeBuilder<Comment> builder)
		{
			builder.ToTable("tblComments");
			builder.Property(p => p.Id).HasColumnName("CommentId");
		}
	}
}
