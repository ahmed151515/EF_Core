using L13_Mapping_Strategies.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace L13_Mapping_Strategies.Data.Config
{
	public class QuizConfig : IEntityTypeConfiguration<Quiz>
	{
		public void Configure(EntityTypeBuilder<Quiz> builder)
		{
			builder.HasKey(p => p.Id);
			builder.Property(p => p.Id).ValueGeneratedNever();

			builder.UseTpcMappingStrategy();
		}
	}
}
