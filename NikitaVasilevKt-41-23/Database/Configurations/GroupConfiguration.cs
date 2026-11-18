using NikitaVasilevKt_41_23.Database.Helpers;
using NikitaVasilevKt_41_23.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NikitaVasilevKt_41_23.Database.Configurations
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        private const string TableName = "cd_group";

        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.HasKey(p => p.GroupId)
                .HasName($"pk_{TableName}_group_id");

            builder.Property(p => p.GroupId)
                .ValueGeneratedOnAdd()
                .HasColumnName("group_id")
                .HasComment("Идентификатор записи группы");

            builder.Property(p => p.GroupName)
                .IsRequired()
                .HasColumnName("c_group_name")
                .HasColumnType(ColumnType.String).HasMaxLength(100)
                .HasComment("Название группы");

            builder.Property(p => p.Speciality)
                .IsRequired()
                .HasColumnName("c_group_speciality")
                .HasColumnType(ColumnType.String).HasMaxLength(150)
                .HasComment("Специальность");

            builder.Property(p => p.AdmissionYear)
                .IsRequired()
                .HasColumnName("c_group_admission_year")
                .HasColumnType(ColumnType.Int)
                .HasComment("Год поступления");

            builder.Property(p => p.IsDeleted)
                .IsRequired()
                .HasColumnName("b_deleted")
                .HasColumnType(ColumnType.Bool)
                .HasComment("Статус удаления");

            builder.ToTable(TableName);
        }
    }
}
