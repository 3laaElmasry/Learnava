using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Learnava.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class PositionCoulumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Position",
                table: "Videos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Position",
                table: "Videos");
        }
    }
}
