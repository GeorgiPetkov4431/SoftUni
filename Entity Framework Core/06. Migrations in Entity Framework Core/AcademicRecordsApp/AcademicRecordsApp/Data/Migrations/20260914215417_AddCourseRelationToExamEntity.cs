using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcademicRecordsApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCourseRelationToExamEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK__Students__3214EC07F540558B",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Grades__3214EC07B60F8E6B",
                table: "Grades");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Exams__3214EC07DD612108",
                table: "Exams");

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "Exams",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Grades",
                table: "Grades",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Exams",
                table: "Exams",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Exams_CourseId",
                table: "Exams",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_Courses_CourseId",
                table: "Exams",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exams_Courses_CourseId",
                table: "Exams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Grades",
                table: "Grades");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Exams",
                table: "Exams");

            migrationBuilder.DropIndex(
                name: "IX_Exams_CourseId",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Exams");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Students__3214EC07F540558B",
                table: "Students",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Grades__3214EC07B60F8E6B",
                table: "Grades",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Exams__3214EC07DD612108",
                table: "Exams",
                column: "Id");
        }
    }
}
