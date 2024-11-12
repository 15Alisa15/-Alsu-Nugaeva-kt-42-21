using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Nugaeva_Alsu_OZKT_42_21.Database.Helpers;
using Nugaeva_Alsu_OZKT_42_21.Database.Models;
using Nugaeva_Alsu_OZKT_42_21.Database.Configurations;


namespace Nugaeva_Alsu_OZKT_42_21.Database.Configurations
{

    public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
    {
        private const string TableName = "Subject";

        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            // Задаем первичный ключ
            builder.HasKey(p => p.SubjectId)
                   .HasName($"pk_{TableName}_Id");

            // Для целочисленного первичного ключа задаем автогенерацию (к каждой новой записи будет добавлять +1)
            builder.Property(p => p.SubjectId)
                  .ValueGeneratedOnAdd();

            // Расписываем как будут называться колонок в БД, а так же их обязательность и т.д.
            builder.Property(p => p.SubjectId)
                  .HasColumnName("Id")
                  .HasComment("Идентификатор записи дисциплины");

            builder.Property(p => p.SubjectName)
                  .IsRequired()
                  .HasColumnName("c_Subjectname")
                  .HasColumnType(ColumnType.String).HasMaxLength(100)
                  .HasComment("Название предмета");

            builder.Property(p => p.IsDeleted)
                  .IsRequired()
                  .HasColumnName("IsDeleted")
                  .HasColumnType(ColumnType.Bool)
                  .HasComment("Статус удаления");

            builder.ToTable(TableName);

        /*    // Настройка связи с Group
            builder.HasMany(s => s.Groups)
                     .WithOne(g => g.Subject)
                     .HasForeignKey(g => g.SubjectId)
                     .HasConstraintName("fk_f_subject_id")
                     .OnDelete(DeleteBehavior.Restrict);*/

            
            // Добавляем индекс для SubjectId
           /* builder.HasIndex(p => p.SubjectId, $"idx_{TableName}_subject_id");*/
        }
    }
}