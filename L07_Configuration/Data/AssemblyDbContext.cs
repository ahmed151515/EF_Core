﻿using L07_Configuration.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
namespace L07_Configuration.Data
{
	public class AssemblyDbContext : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Comment> Comments { get; set; }
		public DbSet<Tweet> Tweets { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);

			var v1ConnectionString = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json").Build()
				.GetSection("v1").Value;

			optionsBuilder.UseSqlServer(v1ConnectionString);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfigurationsFromAssembly(typeof(Config.UserConfig).Assembly);
		}

	}

}
