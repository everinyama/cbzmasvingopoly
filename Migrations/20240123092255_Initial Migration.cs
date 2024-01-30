using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BillPayments_LookUp_Validation.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblPendings",
                columns: table => new
                {
                    ID = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransRef = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bank = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExternalTransRef = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProcessDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPendings", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblPendings");
        }
    }
}
