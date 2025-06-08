using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PromoCodeFactory.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEmployeeMobile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "mobile",
                table: "EmployeeDbSet",
                type: "TEXT",
                maxLength: 50,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "EmployeeDbSet",
                keyColumn: "Id",
                keyValue: new Guid("451533d5-d8d5-4a11-9c7b-eb9f14e1a32f"),
                column: "mobile",
                value: null);

            migrationBuilder.UpdateData(
                table: "EmployeeDbSet",
                keyColumn: "Id",
                keyValue: new Guid("f766e2bf-340a-46ea-bff3-f1700b435895"),
                column: "mobile",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "mobile",
                table: "EmployeeDbSet");
        }
    }
}
