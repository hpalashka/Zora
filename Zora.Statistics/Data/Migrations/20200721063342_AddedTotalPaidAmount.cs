using Microsoft.EntityFrameworkCore.Migrations;

namespace Zora.Statistics.Data.Migrations
{
    public partial class AddedTotalPaidAmount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TotalPaidAmount",
                table: "Statistics",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPaidAmount",
                table: "Statistics");
        }
    }
}
