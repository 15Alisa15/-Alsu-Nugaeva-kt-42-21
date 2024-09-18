using Microsoft.EntityFrameworkCore;
using Nugaeva_Alsu_OZKT_42_21.Database.Configurations;
using Nugaeva_Alsu_OZKT_42_21.Database.Models;
using Nugaeva_Alsu_OZKT_42_21.Database;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;
using System.IO;
using Npgsql.EntityFrameworkCore;

public class NugaevaDbContext : DbContext
{
	public DbSet<Student> Students { get; set; }
	public DbSet<Nugaeva_Alsu_OZKT_42_21.Database.Models.Group> Groups { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		if (!optionsBuilder.IsConfigured)
		{
			var configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
				.Build();

			optionsBuilder.UseNpgsql(configuration.GetConnectionString("NugaevaDbContextConnection"));
		}
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfiguration(new StudentConfiguration());
		modelBuilder.ApplyConfiguration(new GroupConfiguration());

		// Остальной код остается без изменений
	}

	public NugaevaDbContext(DbContextOptions<NugaevaDbContext> options) : base(options)
	{
	}
}