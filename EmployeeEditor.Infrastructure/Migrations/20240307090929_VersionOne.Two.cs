﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeEditor.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class VersionOneTwo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Employees_Email",
                table: "Employees",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Employees_Email",
                table: "Employees");
        }
    }
}
