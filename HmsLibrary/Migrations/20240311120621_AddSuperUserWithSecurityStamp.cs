using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HmsLibrary.Migrations
{
    /// <inheritdoc />
    public partial class AddSuperUserWithSecurityStamp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { new Guid("258ab84b-1b19-4bb7-9df3-d614df962b44"), null, "Employee", "EMPLOYEE" },
                    { new Guid("2a6d831f-f59c-4e30-8df3-e293f4a04df2"), null, "Patient", "PATIENT" },
                    { new Guid("6386866b-5c71-480d-8cce-7f3cb73a5ecf"), null, "Nurse", "NURSE" },
                    { new Guid("6f3293a5-0284-422a-a17d-d82b42437204"), null, "Doctor", "DOCTOR" },
                    { new Guid("95a2802d-0c74-4e25-94fb-fec97d9abb49"), null, "Receptionist", "RECEPTIONIST" },
                    { new Guid("a8b4be3d-d6b6-4b7d-806b-143e1a879570"), null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("c21d9a0a-7aa8-49d2-80b0-633a8899e0c0"), 0, "5bd5da04-a8bd-465d-8b6f-f20f2c3c109d", null, false, false, null, null, "ADMIN", "AQAAAAIAAYagAAAAEPhfi/h5ugwLDspuCK5nJyaD6SAMuTxAoplkV+tQyzuKAVedJLvWrFgsLbR4nCBTxQ==", null, false, "6a01eebf-ed7f-4ad0-8a2e-a014d12ab27e", false, "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("258ab84b-1b19-4bb7-9df3-d614df962b44"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2a6d831f-f59c-4e30-8df3-e293f4a04df2"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("6386866b-5c71-480d-8cce-7f3cb73a5ecf"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("6f3293a5-0284-422a-a17d-d82b42437204"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("95a2802d-0c74-4e25-94fb-fec97d9abb49"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a8b4be3d-d6b6-4b7d-806b-143e1a879570"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c21d9a0a-7aa8-49d2-80b0-633a8899e0c0"));

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
    }
}
