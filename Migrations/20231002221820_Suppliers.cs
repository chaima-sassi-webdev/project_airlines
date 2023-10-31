using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace project_airlines.Migrations
{
    public partial class Suppliers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GlobalTraffic",
                columns: table => new
                {
                    YEAR = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FLOWN = table.Column<int>(type: "int", nullable: false),
                    BOOKED = table.Column<int>(type: "int", nullable: false),
                    CR = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlobalTraffic", x => x.YEAR);
                });

            migrationBuilder.CreateTable(
                name: "Market",
                columns: table => new
                {
                    escale_D = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    zonegeo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    zonegeoarabe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pays = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    paysArabe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    paysArRapport = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Market", x => x.escale_D);
                });

            migrationBuilder.CreateTable(
                name: "Market_Details",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AC_REGISTRATION = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DAY_OF_ORIGIN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PAX_FLOWN_C = table.Column<int>(type: "int", nullable: false),
                    PAX_FLOWN_Y = table.Column<int>(type: "int", nullable: false),
                    FN_CARRIER = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FN_NUMBER = table.Column<int>(type: "int", nullable: false),
                    ARR_AP_ACTUAL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PAX_BOOKED_C = table.Column<int>(type: "int", nullable: false),
                    PAX_BOOKED_Y = table.Column<int>(type: "int", nullable: false),
                    ESCALESTAT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AVG_FARE = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Market_Details", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    mobile_phone = table.Column<long>(type: "bigint", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupplierImage = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GlobalTraffic");

            migrationBuilder.DropTable(
                name: "Market");

            migrationBuilder.DropTable(
                name: "Market_Details");

            migrationBuilder.DropTable(
                name: "Suppliers");
        }
    }
}
