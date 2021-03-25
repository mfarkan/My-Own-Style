using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.DataLayer.Migrations
{
    public partial class SectorMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Customers_CustomerId",
                schema: "public",
                table: "Expenses");

            migrationBuilder.DropTable(
                name: "Customers",
                schema: "public");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_CustomerId",
                schema: "public",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                schema: "public",
                table: "Expenses");

            migrationBuilder.AddColumn<Guid>(
                name: "BankAccountId",
                schema: "public",
                table: "Expenses",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SectorId",
                schema: "public",
                table: "Expenses",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BankAccount",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedByIp = table.Column<string>(nullable: true),
                    CreatedByUserName = table.Column<string>(nullable: true),
                    BankAccountName = table.Column<string>(nullable: true),
                    BankAccountDescription = table.Column<string>(nullable: true),
                    BankType = table.Column<int>(nullable: false),
                    TotalBalance = table.Column<decimal>(nullable: false),
                    AccountIBAN = table.Column<string>(nullable: true),
                    AccountType = table.Column<int>(nullable: false),
                    AccountNo = table.Column<string>(nullable: true),
                    CurrencyType = table.Column<int>(nullable: false),
                    UsableBalance = table.Column<decimal>(nullable: false),
                    BlockedBalance = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccount", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sectors",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Status = table.Column<int>(nullable: false, defaultValue: 1),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedByIp = table.Column<string>(nullable: true),
                    CreatedByUserName = table.Column<string>(nullable: true),
                    SectorDescription = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sectors", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_BankAccountId",
                schema: "public",
                table: "Expenses",
                column: "BankAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_SectorId",
                schema: "public",
                table: "Expenses",
                column: "SectorId");

            migrationBuilder.CreateIndex(
                name: "IX_Sectors_CreatedAt",
                schema: "public",
                table: "Sectors",
                column: "CreatedAt");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_BankAccount_BankAccountId",
                schema: "public",
                table: "Expenses",
                column: "BankAccountId",
                principalSchema: "public",
                principalTable: "BankAccount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Sectors_SectorId",
                schema: "public",
                table: "Expenses",
                column: "SectorId",
                principalSchema: "public",
                principalTable: "Sectors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_BankAccount_BankAccountId",
                schema: "public",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Sectors_SectorId",
                schema: "public",
                table: "Expenses");

            migrationBuilder.DropTable(
                name: "BankAccount",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Sectors",
                schema: "public");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_BankAccountId",
                schema: "public",
                table: "Expenses");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_SectorId",
                schema: "public",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "BankAccountId",
                schema: "public",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "SectorId",
                schema: "public",
                table: "Expenses");

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                schema: "public",
                table: "Expenses",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Customers",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedByIp = table.Column<string>(type: "text", nullable: true),
                    CreatedByUserName = table.Column<string>(type: "text", nullable: true),
                    CustomerAddress = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    CustomerCompanyType = table.Column<int>(type: "integer", maxLength: 255, nullable: false),
                    CustomerDescription = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    CustomerEmailAddress = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    CustomerName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    CustomerTelephoneNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    InstitutionId = table.Column<Guid>(type: "uuid", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_Institution_InstitutionId",
                        column: x => x.InstitutionId,
                        principalSchema: "public",
                        principalTable: "Institution",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_CustomerId",
                schema: "public",
                table: "Expenses",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CreatedAt",
                schema: "public",
                table: "Customers",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_InstitutionId",
                schema: "public",
                table: "Customers",
                column: "InstitutionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Customers_CustomerId",
                schema: "public",
                table: "Expenses",
                column: "CustomerId",
                principalSchema: "public",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
