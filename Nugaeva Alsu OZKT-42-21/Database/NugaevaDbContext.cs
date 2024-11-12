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
using Group = Nugaeva_Alsu_OZKT_42_21.Database.Models.Group;

public class NugaevaDbContext : DbContext

{
    //Добавляем таблицы
    public DbSet<Student> Students{ get; set; }
    public DbSet<Group> Groups { get; set; }
    //Nugaeva_Alsu_OZKT_42_21.Database.Models.
    public DbSet<Subject> Subjects { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Student>()
            .Property(p => p.StudentId)
            .ValueGeneratedOnAdd();

        modelBuilder.ApplyConfiguration(new StudentConfiguration());
        modelBuilder.ApplyConfiguration(new GroupConfiguration());
        modelBuilder.ApplyConfiguration(new SubjectConfiguration());


    }



    public NugaevaDbContext(DbContextOptions<NugaevaDbContext> options) : base(options)
    {
    }
}