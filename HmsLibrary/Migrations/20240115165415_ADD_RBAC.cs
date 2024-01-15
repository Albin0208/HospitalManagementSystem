using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HmsLibrary.Migrations
{
    /// <inheritdoc />
    public partial class ADD_RBAC : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Discriminator",
                table: "Employees",
                newName: "Role");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Role",
                table: "Employees",
                newName: "Discriminator");
        }
    }
}
