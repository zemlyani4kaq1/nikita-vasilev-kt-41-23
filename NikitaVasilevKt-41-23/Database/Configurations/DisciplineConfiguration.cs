using NikitaVasilevKt_41_23.Database.Helpers;
using NikitaVasilevKt_41_23.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NikitaVasilevKt_41_23.Database.Configurations
{
    public class DisciplineConfiguration : IEntityTypeConfiguration<Discipline>
    {
        private const string TableName = "cd_discipline";

        public void Configure(EntityTypeBuilder<Discipline> builder)
        {
            builder.HasKey(p => p.DisciplineId)
                .HasName($"pk_{TableName}_discipline_id");

            builder.Property(p => p.DisciplineId)
                .ValueGeneratedOnAdd()
                .HasColumnName("discipline_id")
                .HasComment("Идентификатор дисциплины");

            builder.Property(p => p.DisciplineName)
                .IsRequired()
                .HasColumnName("c_discipline_name")
                .HasColumnType(ColumnType.String).HasMaxLength(150)
                .HasComment("Название предмета");

            builder.Property(p => p.Direction)
                .IsRequired()
                .HasColumnName("c_discipline_direction")
                .HasColumnType(ColumnType.String).HasMaxLength(100)
                .HasComment("Направление (гуманитарное/техническое)");

            builder.Property(p => p.IsDeleted)
                .IsRequired()
                .HasColumnName("b_deleted")
                .HasColumnType(ColumnType.Bool)
                .HasComment("Статус удаления");

            builder.ToTable(TableName);
        }
    }
}
