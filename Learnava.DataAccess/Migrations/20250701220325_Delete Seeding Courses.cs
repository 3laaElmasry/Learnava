using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Learnava.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class DeleteSeedingCourses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: -3);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: -1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "CreatedAt", "Description", "ImgUrl", "InstructorId", "Title" },
                values: new object[,]
                {
                    { -3, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Master Object-Oriented Programming fundamentals", "", "03b35f2f-b6c6-48f8-8616-1ff545e6a864", "OOP" },
                    { -2, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Learn Algorithms and apply them in practice", "", "c0241e9f-63de-4005-9508-be5d2c1fb8e7", "Algorithms" },
                    { -1, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Learn Data Structures and use them to solve real world problems", "", "e4d92a7b-15a4-4f68-a0f4-de7c0ea63064", "Data Structures" }
                });
        }
    }
}
