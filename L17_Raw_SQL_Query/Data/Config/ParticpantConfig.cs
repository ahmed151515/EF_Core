﻿using L17_Raw_SQL_Query.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace L17_Raw_SQL_Query.Data.Config
{
	public class ParticipantConfig : IEntityTypeConfiguration<Participant>
	{
		public void Configure(EntityTypeBuilder<Participant> builder)
		{
			builder.HasKey(c => c.Id);
			builder.Property(p => p.Id).ValueGeneratedNever();

			builder.Property(p => p.FName).HasColumnType("varchar").HasMaxLength(50).IsRequired();
			builder.Property(p => p.LName).HasColumnType("varchar").HasMaxLength(50).IsRequired();




			builder.ToTable("Particpants");
		}



	}
	internal class IndividualConfiguration : IEntityTypeConfiguration<Individual>
	{
		public void Configure(EntityTypeBuilder<Individual> builder)
		{
			builder.ToTable("Individuals");
		}
	}

	internal class CoporateConfiguration : IEntityTypeConfiguration<Coporate>
	{
		public void Configure(EntityTypeBuilder<Coporate> builder)
		{
			builder.ToTable("Coporates");
		}
	}
}
