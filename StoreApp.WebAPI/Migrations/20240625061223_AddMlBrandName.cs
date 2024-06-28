using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreApp.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddMlBrandName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Ml1Name",
                table: "Brands",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Ml2Name",
                table: "Brands",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Ml1Name", "Ml2Name" },
                values: new object[] { null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ml1Name",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "Ml2Name",
                table: "Brands");
        }
    }
}
