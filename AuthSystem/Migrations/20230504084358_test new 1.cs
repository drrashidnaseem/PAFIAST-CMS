using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthSystem.Migrations
{
    /// <inheritdoc />
    public partial class testnew1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TestId1",
                table: "Tests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TestId",
                table: "Subjects",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tests_TestId1",
                table: "Tests",
                column: "TestId1");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_TestId",
                table: "Subjects",
                column: "TestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Tests_TestId",
                table: "Subjects",
                column: "TestId",
                principalTable: "Tests",
                principalColumn: "TestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_Tests_TestId1",
                table: "Tests",
                column: "TestId1",
                principalTable: "Tests",
                principalColumn: "TestId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Tests_TestId",
                table: "Subjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Tests_Tests_TestId1",
                table: "Tests");

            migrationBuilder.DropIndex(
                name: "IX_Tests_TestId1",
                table: "Tests");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_TestId",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "TestId1",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "TestId",
                table: "Subjects");
        }
    }
}
