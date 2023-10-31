using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace project_airlines.Migrations
{
    public partial class TopCountries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TopCountries",
                table: "TopCountries");

            migrationBuilder.AlterColumn<string>(
                name: "Zonegeo",
                table: "TopCountries",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "TopCountries",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TopCountries",
                table: "TopCountries",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TopCountries",
                table: "TopCountries");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TopCountries");

            migrationBuilder.AlterColumn<string>(
                name: "Zonegeo",
                table: "TopCountries",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TopCountries",
                table: "TopCountries",
                column: "Zonegeo");
        }
    }
}
