using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce_MVC.Migrations
{
    /// <inheritdoc />
    public partial class IsRegisterverfy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRegisterVerification",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRegisterVerification",
                table: "AspNetUsers");
        }
    }
}
