using L17_Raw_SQL_Query.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace L17_Raw_SQL_Query.Data.Config
{
	public class SectionDetailsConfig : IEntityTypeConfiguration<SectionDetails>
	{
		public void Configure(EntityTypeBuilder<SectionDetails> builder)
		{
			builder.HasNoKey();

		}

	}

}
