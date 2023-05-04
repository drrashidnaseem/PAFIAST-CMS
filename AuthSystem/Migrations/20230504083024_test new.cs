using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthSystem.Migrations
{
    /// <inheritdoc />
    public partial class testnew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tests_Tests_TestId1",
                table: "Tests");

            migrationBuilder.DropIndex(
                name: "IX_Tests_TestId1",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "TestId1",
                table: "Tests");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TestId1",
                table: "Tests",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tests_TestId1",
                table: "Tests",
                column: "TestId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_Tests_TestId1",
                table: "Tests",
                column: "TestId1",
                principalTable: "Tests",
                principalColumn: "TestId");
        }
    }
}
