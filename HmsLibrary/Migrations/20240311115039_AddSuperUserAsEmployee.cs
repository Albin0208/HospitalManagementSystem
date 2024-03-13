﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HmsLibrary.Migrations
{
    /// <inheritdoc />
    public partial class AddSuperUserAsEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Employees");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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
    }
}