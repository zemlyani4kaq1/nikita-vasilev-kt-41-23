using NikitaVasilevKt_41_23.Database.Helpers;
using NikitaVasilevKt_41_23.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NikitaVasilevKt_41_23.Database.Configurations
{
    public class GradeConfiguration : IEntityTypeConfiguration<Grade>
    {
        private const string TableName = "cd_grade";

        public void Configure(EntityTypeBuilder<Grade> builder)
        {
            builder.HasKey(p => p.GradeId)
                .HasName($"pk_{TableName}_grade_id");

            builder.Property(p => p.GradeId)
                .ValueGeneratedOnAdd()
                .HasColumnName("grade_id")
                .HasComment("Идентификатор оценки");

            builder.Property(p => p.Value)
                .IsRequired()
                .HasColumnName("c_grade_value")
                .HasColumnType(ColumnType.Int)
                .HasComment("Оценка");

            builder.Property(p => p.StudentId)
                .IsRequired()
                .HasColumnName("f_student_id")
                .HasColumnType(ColumnType.Int);

            builder.Property(p => p.DisciplineId)
                .IsRequired()
                .HasColumnName("f_discipline_id")
                .HasColumnType(ColumnType.Int);

            builder.ToTable(TableName)
                .HasOne(p => p.Student)
                .WithMany()
                .HasForeignKey(p => p.StudentId)
                .HasConstraintName("fk_grade_student_id")
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable(TableName)
                .HasOne(p => p.Discipline)
                .WithMany()
                .HasForeignKey(p => p.DisciplineId)
                .HasConstraintName("fk_grade_discipline_id")
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable(TableName)
                .HasIndex(p => p.StudentId, $"idx_{TableName}_fk_student_id");

            builder.ToTable(TableName)
                .HasIndex(p => p.DisciplineId, $"idx_{TableName}_fk_discipline_id");

            builder.Navigation(p => p.Student).AutoInclude();
            builder.Navigation(p => p.Discipline).AutoInclude();
        }
    }
}
