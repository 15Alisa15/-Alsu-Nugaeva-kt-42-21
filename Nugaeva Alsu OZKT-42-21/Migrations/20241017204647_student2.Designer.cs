﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Nugaeva_Alsu_OZKT_42_21.Migrations
{
    [DbContext(typeof(NugaevaDbContext))]
    [Migration("20241017204647_student2")]
    partial class student2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Nugaeva_Alsu_OZKT_42_21.Database.Models.Group", b =>
                {
                    b.Property<int>("GroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Идентификатор записи группы")
                        .HasComment("Идентификатор записи группы");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GroupId"));

                    b.Property<string>("GroupName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(Max)")
                        .HasColumnName("Название группы");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("IsDeleted")
                        .HasComment("Статус удаления");

                    b.Property<int?>("SubjectId")
                        .HasColumnType("int");

                    b.HasKey("GroupId")
                        .HasName("pk_Groups_Id");

                    b.HasIndex("SubjectId");

                    b.ToTable("Groups", (string)null);
                });

            modelBuilder.Entity("Nugaeva_Alsu_OZKT_42_21.Database.Models.Subject", b =>
                {
                    b.Property<int>("SubjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id")
                        .HasComment("Идентификатор записи дисциплины");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SubjectId"));

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("IsDeleted")
                        .HasComment("Статус удаления");

                    b.Property<string>("SubjectName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(Max)")
                        .HasColumnName("c_Subjectname")
                        .HasComment("Название предмета");

                    b.HasKey("SubjectId")
                        .HasName("pk_Subject_Id");

                    b.ToTable("Subject", (string)null);
                });

            modelBuilder.Entity("Student", b =>
                {
                    b.Property<int>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id")
                        .HasComment("Идентификатор записи студента");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StudentId"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(Max)")
                        .HasColumnName("c_student_firstname")
                        .HasComment("Имя студента");

                    b.Property<int?>("GroupId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("IsDeleted")
                        .HasComment("Статус удаления");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(Max)")
                        .HasColumnName("c_student_lastname")
                        .HasComment("Фамилия студента");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(Max)")
                        .HasColumnName("c_student_middlename")
                        .HasComment("Отчество студента");

                    b.HasKey("StudentId")
                        .HasName("pk_Students_Id");

                    b.HasIndex("GroupId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Nugaeva_Alsu_OZKT_42_21.Database.Models.Group", b =>
                {
                    b.HasOne("Nugaeva_Alsu_OZKT_42_21.Database.Models.Subject", "Subject")
                        .WithMany("Groups")
                        .HasForeignKey("SubjectId");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("Student", b =>
                {
                    b.HasOne("Nugaeva_Alsu_OZKT_42_21.Database.Models.Group", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId");

                    b.Navigation("Group");
                });

            modelBuilder.Entity("Nugaeva_Alsu_OZKT_42_21.Database.Models.Subject", b =>
                {
                    b.Navigation("Groups");
                });
#pragma warning restore 612, 618
        }
    }
}
