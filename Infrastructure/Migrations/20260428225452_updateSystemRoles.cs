using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateSystemRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TechnicianCategories_Categories_CategoryId",
                table: "TechnicianCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_TechnicianCategories_Users_TechnicianId",
                table: "TechnicianCategories");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("d1a2b3c4-e5f6-4a7b-8c9d-0e1f2a3b4c5d"),
                column: "Code",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("e2a3b4c5-d6f7-4b8c-9d0e-1f2a3b4c5d6e"),
                column: "Code",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("f3a4b5c6-e7d8-4c9d-0e1f-2a3b4c5d6e7f"),
                column: "Code",
                value: 3);

            migrationBuilder.AddForeignKey(
                name: "FK_TechnicianCategories_Categories_CategoryId",
                table: "TechnicianCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TechnicianCategories_Users_TechnicianId",
                table: "TechnicianCategories",
                column: "TechnicianId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TechnicianCategories_Categories_CategoryId",
                table: "TechnicianCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_TechnicianCategories_Users_TechnicianId",
                table: "TechnicianCategories");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("d1a2b3c4-e5f6-4a7b-8c9d-0e1f2a3b4c5d"),
                column: "Code",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("e2a3b4c5-d6f7-4b8c-9d0e-1f2a3b4c5d6e"),
                column: "Code",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("f3a4b5c6-e7d8-4c9d-0e1f-2a3b4c5d6e7f"),
                column: "Code",
                value: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_TechnicianCategories_Categories_CategoryId",
                table: "TechnicianCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TechnicianCategories_Users_TechnicianId",
                table: "TechnicianCategories",
                column: "TechnicianId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
