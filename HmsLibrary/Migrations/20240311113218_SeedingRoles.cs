using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HmsLibrary.Migrations
{
    /// <inheritdoc />
    public partial class SeedingRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EmployeeRoles",
                keyColumn: "Id",
                keyValue: new Guid("0e8e197b-c018-47df-8c64-9317b5945112"));

            migrationBuilder.DeleteData(
                table: "EmployeeRoles",
                keyColumn: "Id",
                keyValue: new Guid("88151fee-8564-4f9c-8bf6-a93d5d5e5dab"));

            migrationBuilder.DeleteData(
                table: "EmployeeRoles",
                keyColumn: "Id",
                keyValue: new Guid("ac76b17f-30fe-44d1-adb3-7600ea9da648"));

            migrationBuilder.DeleteData(
                table: "EmployeeRoles",
                keyColumn: "Id",
                keyValue: new Guid("adeb0de7-0b7d-4804-b1da-509888e32ba9"));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                table: "EmployeeRoles",
                columns: new[] { "Id", "RoleName" },
                values: new object[,]
                {
                    { new Guid("0e8e197b-c018-47df-8c64-9317b5945112"), "Nurse" },
                    { new Guid("88151fee-8564-4f9c-8bf6-a93d5d5e5dab"), "Doctor" },
                    { new Guid("ac76b17f-30fe-44d1-adb3-7600ea9da648"), "Receptionist" },
                    { new Guid("adeb0de7-0b7d-4804-b1da-509888e32ba9"), "Admin" }
                });
        }
    }
}
