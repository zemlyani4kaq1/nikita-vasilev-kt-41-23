using NikitaVasilevKt_41_23.Database.Helpers;
using NikitaVasilevKt_41_23.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NikitaVasilevKt_41_23.Database.Configurations
{
    public class CreditConfiguration : IEntityTypeConfiguration<Credit>
    {
        private const string TableName = "cd_credit";

        public void Configure(EntityTypeBuilder<Credit> builder)
        {
            builder.HasKey(p => p.CreditId)
                .HasName($"pk_{TableName}_credit_id");

            builder.Property(p => p.CreditId)
                .ValueGeneratedOnAdd()
                .HasColumnName("credit_id")
                .HasComment("Идентификатор зачета");

            builder.Property(p => p.IsPassed)
                .IsRequired()
                .HasColumnName("b_is_passed")
                .HasColumnType(ColumnType.Bool)
                .HasComment("Отметка о сдаче зачета");

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
                .HasConstraintName("fk_credit_student_id")
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable(TableName)
                .HasOne(p => p.Discipline)
                .WithMany()
                .HasForeignKey(p => p.DisciplineId)
                .HasConstraintName("fk_credit_discipline_id")
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
