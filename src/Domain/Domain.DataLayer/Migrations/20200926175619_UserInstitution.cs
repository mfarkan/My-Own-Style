using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.DataLayer.Migrations
{
    public partial class UserInstitution : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "InstitutionId",
                schema: "public",
                table: "AspNetRoles",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_InstitutionId",
                schema: "public",
                table: "AspNetRoles",
                column: "InstitutionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_Institution_InstitutionId",
                schema: "public",
                table: "AspNetRoles",
                column: "InstitutionId",
                principalSchema: "public",
                principalTable: "Institution",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_Institution_InstitutionId",
                schema: "public",
                table: "AspNetRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRoles_InstitutionId",
                schema: "public",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "InstitutionId",
                schema: "public",
                table: "AspNetRoles");
        }
    }
}
