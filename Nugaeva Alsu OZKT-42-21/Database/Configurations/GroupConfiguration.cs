using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using Nugaeva_Alsu_OZKT_42_21.Database.Helpers;
using Nugaeva_Alsu_OZKT_42_21.Database.Models;
using Nugaeva_Alsu_OZKT_42_21.Database.Configurations;


using Group = Nugaeva_Alsu_OZKT_42_21.Database.Models.Group;

namespace Nugaeva_Alsu_OZKT_42_21.Database.Configurations
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        private const string TableName = "Groups";


        public void Configure(EntityTypeBuilder<Group> builder)
        {
            // Задаем первичный ключ
            builder.HasKey(p => p.GroupId)
                
                   .HasName($"pk_{TableName}_Id");

            // Для целочисленного первичного ключа задаем автогенерацию (к каждой новой записи будет добавлять +1)
            builder.Property(p => p.GroupId)
                  .ValueGeneratedOnAdd();

            // Расписываем как будут называться колонки в БД, а так же их обязательность и т.д.
            builder.Property(p => p.GroupId)
                  .HasColumnName("Идентификатор записи группы")
                  .HasComment("Идентификатор записи группы");

            builder.Property(p => p.GroupName)
                  .IsRequired()
                  .HasColumnName("Название группы")
                  .HasColumnType(ColumnType.String).HasMaxLength(100)
                  /*.HasComment("Название группы")*/;

            builder.Property(p => p.IsDeleted)
                  .IsRequired()
                  .HasColumnName("IsDeleted")
                  .HasColumnType(ColumnType.Bool)
                  .HasComment("Статус удаления");
            

            /*/ Настройка связи с Student
            builder.HasOne(p => p.Student)
                   .WithMany()
                   .HasForeignKey(p => p.GroupId)
                   .HasConstraintName("fk_f_student_id")
                   .OnDelete(DeleteBehavior.Cascade);

            // Добавление индекса для GroupId
            /*builder.HasIndex(p => p.GroupId);*/
            builder.ToTable(TableName);
        }
    }
}
    