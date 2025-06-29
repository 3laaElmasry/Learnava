using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Learnava.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeedingInstructorTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Instructors_ApplicationUserId",
                table: "Instructors");

            migrationBuilder.CreateIndex(
                name: "IX_Instructors_ApplicationUserId",
                table: "Instructors",
                column: "ApplicationUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Instructors_ApplicationUserId",
                table: "Instructors");

            migrationBuilder.CreateIndex(
                name: "IX_Instructors_ApplicationUserId",
                table: "Instructors",
                column: "ApplicationUserId",
                unique: true);
        }
    }
}
