using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.DataLayer.Migrations
{
    public partial class InitialModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Status = table.Column<int>(nullable: false, defaultValue: 1),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedByIp = table.Column<string>(nullable: true),
                    CreatedByUserName = table.Column<string>(nullable: true),
                    CustomerEmailAddress = table.Column<string>(maxLength: 255, nullable: false),
                    CustomerTelephoneNumber = table.Column<string>(maxLength: 50, nullable: true),
                    CustomerName = table.Column<string>(maxLength: 255, nullable: false),
                    CustomerDescription = table.Column<string>(maxLength: 255, nullable: false),
                    CustomerAddress = table.Column<string>(maxLength: 255, nullable: false),
                    CustomerCompanyType = table.Column<int>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CreatedAt",
                table: "Customers",
                column: "CreatedAt");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
