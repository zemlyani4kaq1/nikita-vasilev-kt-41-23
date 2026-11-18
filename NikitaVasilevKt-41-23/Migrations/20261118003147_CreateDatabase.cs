using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace NikitaVasilevKt_41_23.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cd_discipline",
                columns: table => new
                {
                    discipline_id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор дисциплины")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    c_discipline_name = table.Column<string>(type: "varchar", maxLength: 150, nullable: false, comment: "Название предмета"),
                    c_discipline_direction = table.Column<string>(type: "varchar", maxLength: 100, nullable: false, comment: "Направление (гуманитарное/техническое)"),
                    b_deleted = table.Column<bool>(type: "bool", nullable: false, comment: "Статус удаления")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cd_discipline_discipline_id", x => x.discipline_id);
                });

            migrationBuilder.CreateTable(
                name: "cd_group",
                columns: table => new
                {
                    group_id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор записи группы")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    c_group_name = table.Column<string>(type: "varchar", maxLength: 100, nullable: false, comment: "Название группы"),
                    c_group_speciality = table.Column<string>(type: "varchar", maxLength: 150, nullable: false, comment: "Специальность"),
                    c_group_admission_year = table.Column<int>(type: "int4", nullable: false, comment: "Год поступления"),
                    b_deleted = table.Column<bool>(type: "bool", nullable: false, comment: "Статус удаления")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cd_group_group_id", x => x.group_id);
                });

            migrationBuilder.CreateTable(
                name: "cd_student",
                columns: table => new
                {
                    student_id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор записи студента")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    c_student_firstname = table.Column<string>(type: "varchar", maxLength: 100, nullable: false, comment: "Имя студента"),
                    c_student_lastname = table.Column<string>(type: "varchar", maxLength: 100, nullable: false, comment: "Фамилия студента"),
                    c_student_middlename = table.Column<string>(type: "varchar", maxLength: 100, nullable: false, comment: "Отчество студента"),
                    b_deleted = table.Column<bool>(type: "bool", nullable: false, comment: "Статус удаления"),
                    f_group_id = table.Column<int>(type: "int4", nullable: false, comment: "Идентификатор группы")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cd_student_student_id", x => x.student_id);
                    table.ForeignKey(
                        name: "fk_f_group_id",
                        column: x => x.f_group_id,
                        principalTable: "cd_group",
                        principalColumn: "group_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cd_credit",
                columns: table => new
                {
                    credit_id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор зачета")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    b_is_passed = table.Column<bool>(type: "bool", nullable: false, comment: "Отметка о сдаче зачета"),
                    f_student_id = table.Column<int>(type: "int4", nullable: false),
                    f_discipline_id = table.Column<int>(type: "int4", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cd_credit_credit_id", x => x.credit_id);
                    table.ForeignKey(
                        name: "fk_credit_discipline_id",
                        column: x => x.f_discipline_id,
                        principalTable: "cd_discipline",
                        principalColumn: "discipline_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_credit_student_id",
                        column: x => x.f_student_id,
                        principalTable: "cd_student",
                        principalColumn: "student_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cd_grade",
                columns: table => new
                {
                    grade_id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор оценки")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    c_grade_value = table.Column<int>(type: "int4", nullable: false, comment: "Оценка"),
                    f_student_id = table.Column<int>(type: "int4", nullable: false),
                    f_discipline_id = table.Column<int>(type: "int4", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cd_grade_grade_id", x => x.grade_id);
                    table.ForeignKey(
                        name: "fk_grade_discipline_id",
                        column: x => x.f_discipline_id,
                        principalTable: "cd_discipline",
                        principalColumn: "discipline_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_grade_student_id",
                        column: x => x.f_student_id,
                        principalTable: "cd_student",
                        principalColumn: "student_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "idx_cd_credit_fk_discipline_id",
                table: "cd_credit",
                column: "f_discipline_id");

            migrationBuilder.CreateIndex(
                name: "idx_cd_credit_fk_student_id",
                table: "cd_credit",
                column: "f_student_id");

            migrationBuilder.CreateIndex(
                name: "idx_cd_grade_fk_discipline_id",
                table: "cd_grade",
                column: "f_discipline_id");

            migrationBuilder.CreateIndex(
                name: "idx_cd_grade_fk_student_id",
                table: "cd_grade",
                column: "f_student_id");

            migrationBuilder.CreateIndex(
                name: "idx_cd_student_fk_f_group_id",
                table: "cd_student",
                column: "f_group_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cd_credit");

            migrationBuilder.DropTable(
                name: "cd_grade");

            migrationBuilder.DropTable(
                name: "cd_discipline");

            migrationBuilder.DropTable(
                name: "cd_student");

            migrationBuilder.DropTable(
                name: "cd_group");
        }
    }
}
