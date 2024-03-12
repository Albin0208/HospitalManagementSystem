using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HmsLibrary.Migrations
{
    /// <inheritdoc />
    public partial class RemovedCustomRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmployeeRoles_RoleId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_RoleId",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeRoles",
                table: "EmployeeRoles");

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

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Employees");

            migrationBuilder.RenameTable(
                name: "EmployeeRoles",
                newName: "EmployeeRole");

            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeRoleId",
                table: "Employees",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeRole",
                table: "EmployeeRole",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("015a1e1c-6f86-4d77-8286-3854f885c8d5"), null, "Employee", "EMPLOYEE" },
                    { new Guid("2ba90d6e-9067-49e1-a9bd-912c4f83bba4"), null, "Doctor", "DOCTOR" },
                    { new Guid("30104036-9e60-4f1b-8a12-8a1d80bbb472"), null, "Receptionist", "RECEPTIONIST" },
                    { new Guid("58ea414a-33fa-4aba-bed1-640f2e59dc6f"), null, "Patient", "PATIENT" },
                    { new Guid("af596257-c11d-4490-95cf-bcc70fad29ac"), null, "Nurse", "NURSE" },
                    { new Guid("e89c2fed-d493-4557-8d6d-cfb82b17271b"), null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("5de995d2-9b9b-4b43-87bc-6cc4377aa8bc"), 0, "8d751d41-1fe9-4894-a9cc-3ee81b2758c0", null, false, false, null, null, "ADMIN", "AQAAAAIAAYagAAAAELxOVUclFO/fpCg5VLPsDxLMlQi4FaJt5Ne6FYlrVaXWISQ8tO86pe7L06dJwenlQA==", null, false, "d54140eb-9b6b-4884-9339-226200e6f50e", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("e89c2fed-d493-4557-8d6d-cfb82b17271b"), new Guid("5de995d2-9b9b-4b43-87bc-6cc4377aa8bc") });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EmployeeRoleId",
                table: "Employees",
                column: "EmployeeRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EmployeeRole_EmployeeRoleId",
                table: "Employees",
                column: "EmployeeRoleId",
                principalTable: "EmployeeRole",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmployeeRole_EmployeeRoleId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_EmployeeRoleId",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeRole",
                table: "EmployeeRole");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("015a1e1c-6f86-4d77-8286-3854f885c8d5"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2ba90d6e-9067-49e1-a9bd-912c4f83bba4"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("30104036-9e60-4f1b-8a12-8a1d80bbb472"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("58ea414a-33fa-4aba-bed1-640f2e59dc6f"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("af596257-c11d-4490-95cf-bcc70fad29ac"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("e89c2fed-d493-4557-8d6d-cfb82b17271b"), new Guid("5de995d2-9b9b-4b43-87bc-6cc4377aa8bc") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e89c2fed-d493-4557-8d6d-cfb82b17271b"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("5de995d2-9b9b-4b43-87bc-6cc4377aa8bc"));

            migrationBuilder.DropColumn(
                name: "EmployeeRoleId",
                table: "Employees");

            migrationBuilder.RenameTable(
                name: "EmployeeRole",
                newName: "EmployeeRoles");

            migrationBuilder.AddColumn<Guid>(
                name: "RoleId",
                table: "Employees",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeRoles",
                table: "EmployeeRoles",
                column: "Id");

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

            migrationBuilder.CreateIndex(
                name: "IX_Employees_RoleId",
                table: "Employees",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EmployeeRoles_RoleId",
                table: "Employees",
                column: "RoleId",
                principalTable: "EmployeeRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
