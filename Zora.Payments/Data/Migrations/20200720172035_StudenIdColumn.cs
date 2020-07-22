using Microsoft.EntityFrameworkCore.Migrations;

namespace Zora.Payments.Data.Migrations
{
    public partial class StudenIdColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Payments");

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Payments",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Payments");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
