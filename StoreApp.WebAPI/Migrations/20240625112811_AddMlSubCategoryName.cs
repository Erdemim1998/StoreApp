using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreApp.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddMlSubCategoryName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Ml1Name",
                table: "SubCategories",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Ml2Name",
                table: "SubCategories",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 53,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 58,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 59,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 60,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 61,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 62,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 63,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 64,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 65,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 66,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 67,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 68,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 69,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 70,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 71,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 72,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 73,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 74,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 75,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 76,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 77,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 78,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 79,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 80,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 81,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 82,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 83,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 84,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 85,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 86,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 87,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 88,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 89,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 90,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 91,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 92,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 93,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 94,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 95,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 96,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 97,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 98,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 99,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 100,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 102,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 103,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ml1Name",
                table: "SubCategories");

            migrationBuilder.DropColumn(
                name: "Ml2Name",
                table: "SubCategories");
        }
    }
}
