using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HmsLibrary.Migrations
{
    /// <inheritdoc />
    public partial class AddSuperUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0822305c-ebd0-4cfa-ae8e-26f510737874"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2a3b94fe-94dc-40a9-9f79-1237a5e53705"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b5356f90-0c04-4366-bce7-ba7e671847a7"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("c74ca097-f3f7-4a1c-a928-644cc6ca5e5f"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("313c8639-f9a7-4e41-a7fd-0a5ec5773ffe"), null, "Receptionist", "RECEPTIONIST" },
                    { new Guid("48d5f7c7-a6d8-4d07-af9f-830a1cb645bd"), null, "Doctor", "DOCTOR" },
                    { new Guid("563b0253-ed50-4cdc-b7d3-b1f426a00ca9"), null, "Patient", "PATIENT" },
                    { new Guid("5af53a32-c8de-4e2c-a425-5869ac30fca5"), null, "Employee", "EMPLOYEE" },
                    { new Guid("bae55406-904d-4126-a7ea-f3adcf8241b9"), null, "Admin", "ADMIN" },
                    { new Guid("d6aabe96-817c-465c-8a51-e0f3f7430a9f"), null, "Nurse", "NURSE" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("5696ce30-021e-4e99-aa9b-ae90924ae00e"), 0, "9124fece-7ee7-4452-abc9-62bb37459054", "", false, false, null, null, "ADMIN", null, null, false, null, false, "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("313c8639-f9a7-4e41-a7fd-0a5ec5773ffe"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("48d5f7c7-a6d8-4d07-af9f-830a1cb645bd"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("563b0253-ed50-4cdc-b7d3-b1f426a00ca9"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("5af53a32-c8de-4e2c-a425-5869ac30fca5"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("bae55406-904d-4126-a7ea-f3adcf8241b9"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d6aabe96-817c-465c-8a51-e0f3f7430a9f"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("5696ce30-021e-4e99-aa9b-ae90924ae00e"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("0822305c-ebd0-4cfa-ae8e-26f510737874"), null, "Nurse", "NURSE" },
                    { new Guid("2a3b94fe-94dc-40a9-9f79-1237a5e53705"), null, "Doctor", "DOCTOR" },
                    { new Guid("b5356f90-0c04-4366-bce7-ba7e671847a7"), null, "Receptionist", "RECEPTIONIST" },
                    { new Guid("c74ca097-f3f7-4a1c-a928-644cc6ca5e5f"), null, "Admin", "ADMIN" }
                });
        }
    }
}
