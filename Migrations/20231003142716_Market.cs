using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace project_airlines.Migrations
{
    public partial class Market : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "total_pax",
                table: "Market");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "total_pax",
                table: "Market",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
