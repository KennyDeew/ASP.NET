using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PromoCodeFactory.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerDbSet",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    firstname = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    lastname = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    email = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerDbSet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PreferenceDbSet",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreferenceDbSet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleDbSet",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    description = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleDbSet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerPreference",
                columns: table => new
                {
                    CustomerId = table.Column<Guid>(type: "TEXT", nullable: false),
                    PreferenceId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerPreference", x => new { x.CustomerId, x.PreferenceId });
                    table.ForeignKey(
                        name: "FK_CustomerPreference_CustomerDbSet_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "CustomerDbSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerPreference_PreferenceDbSet_PreferenceId",
                        column: x => x.PreferenceId,
                        principalTable: "PreferenceDbSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeDbSet",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    firstname = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    lastname = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    email = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    RoleId = table.Column<Guid>(type: "TEXT", nullable: false),
                    AppliedPromocodesCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeDbSet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeDbSet_RoleDbSet_RoleId",
                        column: x => x.RoleId,
                        principalTable: "RoleDbSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PromoCodeDbSet",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    code = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    serviceinfo = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    BeginDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    partnername = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    PartnerManagerId = table.Column<Guid>(type: "TEXT", nullable: true),
                    CustomerId = table.Column<Guid>(type: "TEXT", nullable: false),
                    PreferenceId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromoCodeDbSet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PromoCodeDbSet_CustomerDbSet_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "CustomerDbSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PromoCodeDbSet_EmployeeDbSet_PartnerManagerId",
                        column: x => x.PartnerManagerId,
                        principalTable: "EmployeeDbSet",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PromoCodeDbSet_PreferenceDbSet_PreferenceId",
                        column: x => x.PreferenceId,
                        principalTable: "PreferenceDbSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CustomerDbSet",
                columns: new[] { "Id", "email", "firstname", "lastname" },
                values: new object[] { new Guid("a6c8c6b1-4349-45b0-ab31-244740aaf0f0"), "ivan_sergeev@mail.ru", "Иван", "Петров" });

            migrationBuilder.InsertData(
                table: "PreferenceDbSet",
                columns: new[] { "Id", "name" },
                values: new object[,]
                {
                    { new Guid("76324c47-68d2-472d-abb8-33cfa8cc0c84"), "Дети" },
                    { new Guid("c4bda62e-fc74-4256-a956-4760b3858cbd"), "Семья" },
                    { new Guid("ef7f299f-92d7-459f-896e-078ed53ef99c"), "Театр" }
                });

            migrationBuilder.InsertData(
                table: "RoleDbSet",
                columns: new[] { "Id", "description", "name" },
                values: new object[,]
                {
                    { new Guid("53729686-a368-4eeb-8bfa-cc69b6050d02"), "Администратор", "Admin" },
                    { new Guid("b0ae7aac-5493-45cd-ad16-87426a5e7665"), "Партнерский менеджер", "PartnerManager" }
                });

            migrationBuilder.InsertData(
                table: "CustomerPreference",
                columns: new[] { "CustomerId", "PreferenceId" },
                values: new object[,]
                {
                    { new Guid("a6c8c6b1-4349-45b0-ab31-244740aaf0f0"), new Guid("76324c47-68d2-472d-abb8-33cfa8cc0c84") },
                    { new Guid("a6c8c6b1-4349-45b0-ab31-244740aaf0f0"), new Guid("c4bda62e-fc74-4256-a956-4760b3858cbd") }
                });

            migrationBuilder.InsertData(
                table: "EmployeeDbSet",
                columns: new[] { "Id", "AppliedPromocodesCount", "email", "firstname", "lastname", "RoleId" },
                values: new object[,]
                {
                    { new Guid("451533d5-d8d5-4a11-9c7b-eb9f14e1a32f"), 5, "owner@somemail.ru", "Иван", "Сергеев", new Guid("53729686-a368-4eeb-8bfa-cc69b6050d02") },
                    { new Guid("f766e2bf-340a-46ea-bff3-f1700b435895"), 10, "andreev@somemail.ru", "Петр", "Андреев", new Guid("b0ae7aac-5493-45cd-ad16-87426a5e7665") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPreference_PreferenceId",
                table: "CustomerPreference",
                column: "PreferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDbSet_RoleId",
                table: "EmployeeDbSet",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_PromoCodeDbSet_CustomerId",
                table: "PromoCodeDbSet",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_PromoCodeDbSet_PartnerManagerId",
                table: "PromoCodeDbSet",
                column: "PartnerManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_PromoCodeDbSet_PreferenceId",
                table: "PromoCodeDbSet",
                column: "PreferenceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerPreference");

            migrationBuilder.DropTable(
                name: "PromoCodeDbSet");

            migrationBuilder.DropTable(
                name: "CustomerDbSet");

            migrationBuilder.DropTable(
                name: "EmployeeDbSet");

            migrationBuilder.DropTable(
                name: "PreferenceDbSet");

            migrationBuilder.DropTable(
                name: "RoleDbSet");
        }
    }
}
