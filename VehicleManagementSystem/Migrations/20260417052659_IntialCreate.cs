using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class IntialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "staffmodel",
                columns: table => new
                {
                    staffid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_staffmodel", x => x.staffid);
                });

            migrationBuilder.CreateTable(
                name: "usermodel",
                columns: table => new
                {
                    userid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phonenumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    age = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usermodel", x => x.userid);
                });

            migrationBuilder.CreateTable(
                name: "vehiclemodel",
                columns: table => new
                {
                    vehicleid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    licenseplate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    make = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    price = table.Column<double>(type: "float", nullable: false),
                    imagelinkplaintext = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vehiclemodel", x => x.vehicleid);
                });

            migrationBuilder.CreateTable(
                name: "reservationmodel",
                columns: table => new
                {
                    reservationid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userid = table.Column<int>(type: "int", nullable: false),
                    vehicleid = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<double>(type: "float", nullable: false),
                    reservedate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    duedate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    returneddate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reservationmodel", x => x.reservationid);
                    table.ForeignKey(
                        name: "FK_reservationmodel_usermodel_userid",
                        column: x => x.userid,
                        principalTable: "usermodel",
                        principalColumn: "userid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_reservationmodel_vehiclemodel_vehicleid",
                        column: x => x.vehicleid,
                        principalTable: "vehiclemodel",
                        principalColumn: "vehicleid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_reservationmodel_userid",
                table: "reservationmodel",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_reservationmodel_vehicleid",
                table: "reservationmodel",
                column: "vehicleid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "reservationmodel");

            migrationBuilder.DropTable(
                name: "staffmodel");

            migrationBuilder.DropTable(
                name: "usermodel");

            migrationBuilder.DropTable(
                name: "vehiclemodel");
        }
    }
}
