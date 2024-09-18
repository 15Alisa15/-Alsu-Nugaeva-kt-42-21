using Microsoft.EntityFrameworkCore.Migrations;

using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Nugaeva_Alsu_OZKT_42_21.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;


#nullable disable

namespace Nugaeva_Alsu_OZKT_42_21.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cd_group",
                columns: table => new
                {
                    group_id = table.Column<int>(type: "int", nullable: false, comment: "Идентификатор записи группы")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    c_group_name = table.Column<string>(type: "nvarchar(100)", nullable: false, comment: "Название группы")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cd_group_group_id", x => x.group_id);
                });

            migrationBuilder.CreateTable(
                name: "cd_student",
                columns: table => new
                {
                    student_id = table.Column<int>(type: "int", nullable: false, comment: "Идентификатор записи студента")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    c_student_firstname = table.Column<string>(type: "nvarchar(Max)", maxLength: 100, nullable: false, comment: "Имя студента"),
                    c_student_lastname = table.Column<string>(type: "nvarchar(Max)", maxLength: 100, nullable: false, comment: "Фамилия студента"),
                    c_student_middlename = table.Column<string>(type: "nvarchar(Max)", maxLength: 100, nullable: true, comment: "Отчество студента"),
                    f_group_id = table.Column<int>(type: "int", nullable: false, comment: "Идентификатор группы")
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

            migrationBuilder.CreateIndex(
                name: "idx_cd_student_fk_f_group_id",
                table: "cd_student",
                column: "f_group_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cd_student");

            migrationBuilder.DropTable(
                name: "cd_group");
        }
    }
}
