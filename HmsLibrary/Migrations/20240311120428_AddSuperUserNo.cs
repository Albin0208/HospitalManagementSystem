using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HmsLibrary.Migrations
{
    /// <inheritdoc />
    public partial class AddSuperUserNo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("23d07091-1317-4afb-bc31-2ee97ab98b44"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("3858847f-91d3-4741-96b8-c3de72bcb2ef"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("48fbf5ec-cb96-4504-84f3-1874aff2ff94"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4cd6bd04-09c0-4e04-a036-9d4c5027d1c1"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("6d118bbd-8a4f-4784-b082-21dda8d3b174"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a4d80f3e-2966-4764-975b-37c9af5d37df"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("574d7911-cb2d-4aa6-afaf-9e059cdbd059"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("54117611-f277-4a96-b1df-cf39023aaa8b"), null, "Receptionist", "RECEPTIONIST" },
                    { new Guid("58008c00-779e-4a81-a8d7-02229d908923"), null, "Nurse", "NURSE" },
                    { new Guid("7e9e0ff5-7d6b-4fb1-a92c-2e6482047bd1"), null, "Admin", "ADMIN" },
                    { new Guid("991d2dd3-a5fb-4b31-8e1e-38e77fd7d66c"), null, "Employee", "EMPLOYEE" },
                    { new Guid("ed6947a9-6e67-4bc7-bc20-f965d1d8ab2a"), null, "Patient", "PATIENT" },
                    { new Guid("f725641c-cbee-49e6-98dc-8dd3c991357e"), null, "Doctor", "DOCTOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("87ea8871-a3f8-4525-980c-b0fd8eb82516"), 0, "c69ef2be-3f97-4c16-a9d0-876ec76fcb5d", "super@user.com", true, false, null, null, "ADMIN", "AQAAAAIAAYagAAAAEGTCfURi5P9AjAaezlZSaswJTcG6nBaSW3DpVzrl7zPMcd3KTB6yMHExRQnsz/M3Jg==", null, false, null, false, "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("54117611-f277-4a96-b1df-cf39023aaa8b"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("58008c00-779e-4a81-a8d7-02229d908923"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("7e9e0ff5-7d6b-4fb1-a92c-2e6482047bd1"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("991d2dd3-a5fb-4b31-8e1e-38e77fd7d66c"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("ed6947a9-6e67-4bc7-bc20-f965d1d8ab2a"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f725641c-cbee-49e6-98dc-8dd3c991357e"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("87ea8871-a3f8-4525-980c-b0fd8eb82516"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("23d07091-1317-4afb-bc31-2ee97ab98b44"), null, "Patient", "PATIENT" },
                    { new Guid("3858847f-91d3-4741-96b8-c3de72bcb2ef"), null, "Nurse", "NURSE" },
                    { new Guid("48fbf5ec-cb96-4504-84f3-1874aff2ff94"), null, "Admin", "ADMIN" },
                    { new Guid("4cd6bd04-09c0-4e04-a036-9d4c5027d1c1"), null, "Receptionist", "RECEPTIONIST" },
                    { new Guid("6d118bbd-8a4f-4784-b082-21dda8d3b174"), null, "Doctor", "DOCTOR" },
                    { new Guid("a4d80f3e-2966-4764-975b-37c9af5d37df"), null, "Employee", "EMPLOYEE" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("574d7911-cb2d-4aa6-afaf-9e059cdbd059"), 0, "537ce25b-cda1-49fa-95ee-e6e11f342c95", "", false, false, null, null, "ADMIN", "AQAAAAIAAYagAAAAEOIpQFp2Mm/v2QSGqLHjSYoaywhzQ3VaKbc4UNKZga150W99O5oHZGa0QIc1jEp+PQ==", null, false, null, false, "admin" });
        }
    }
}
