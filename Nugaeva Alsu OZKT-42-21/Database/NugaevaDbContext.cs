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
    //Добавляем таблицы
    public DbSet<Student> Students { get; set; }
    public DbSet<Nugaeva_Alsu_OZKT_42_21.Database.Models.Group> Groups;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Добавляем конфигурации к таблицам
        modelBuilder.ApplyConfiguration(new StudentConfiguration());
        modelBuilder.ApplyConfiguration(new GroupConfiguration());
    }

    public NugaevaDbContext(DbContextOptions<NugaevaDbContext> options) : base(options)
    {
    }
}
