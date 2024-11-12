using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Nugaeva_Alsu_OZKT_42_21.Database.Helpers;
using Nugaeva_Alsu_OZKT_42_21.Database.Models;
using Nugaeva_Alsu_OZKT_42_21.Database.Configurations;

namespace Nugaeva_Alsu_OZKT_42_21.Database.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        //Название таблицы, которое будет отображаться в БД
        private const string TableName = "Students";

        public void Configure(EntityTypeBuilder<Student> builder)
        {
            // Задаем первичный ключ
            builder.HasKey(p => p.StudentId)
                   .HasName($"pk_{TableName}_Id");

            // Для целочисленного первичного ключа задаем автогенерацию (к каждой новой записи будет добавлять +1)
            builder.Property(p => p.StudentId)
                 // .ValueGeneratedOnAdd()
/*
            // Расписываем как будут называться колонок в БД, а так же их обязательность и т.д.
            builder.Property(p => p.StudentId)*/
                
                  .HasColumnName("Id")
                  .HasComment("Идентификатор записи студента");

            builder.Property(p => p.FirstName)
                  .IsRequired()
                  .HasColumnName("c_student_firstname")
                  .HasColumnType(ColumnType.String).HasMaxLength(100)
                  .HasComment("Имя студента");

            builder.Property(p => p.LastName)
                  .IsRequired()
                  .HasColumnName("c_student_lastname")
                  .HasColumnType(ColumnType.String).HasMaxLength(100)
                  .HasComment("Фамилия студента");

            builder.Property(p => p.MiddleName)
                  .IsRequired()
                  .HasColumnName("c_student_middlename")
                  .HasColumnType(ColumnType.String).HasMaxLength(100)
                  .HasComment("Отчество студента");

           /* builder.Property(p => p.GroupId)
                  .HasColumnName("group_id")
                  .HasComment("Индетификатор группы")
                  .HasColumnType(ColumnType.Int);*/

            builder.Property(p => p.IsDeleted)
                  .IsRequired()
                  .HasColumnName("IsDeleted")
                  .HasColumnType(ColumnType.Bool)
                  .HasComment("Статус удаления");
/*
            builder.ToTable(TableName)
                   .HasOne(p => p.Group)
                   .WithMany()
                   .HasForeignKey(p => p.StudentId)
                   .HasConstraintName("fk_f_group_id")
                   .OnDelete(DeleteBehavior.Cascade);

            

            builder.ToTable(TableName)
            // Добавляем индекс для информационного поля
            .HasIndex(p => p.GroupId, $"idx_{TableName}_fk_f_group_id");*/


            builder.Navigation(p => p.Group).AutoInclude();

          
        }
    }
}