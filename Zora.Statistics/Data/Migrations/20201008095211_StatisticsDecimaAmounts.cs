using Microsoft.EntityFrameworkCore.Migrations;

namespace Zora.Statistics.Data.Migrations
{
    public partial class StatisticsDecimaAmounts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TotalPaidAmount",
                table: "Statistics",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalAmount",
                table: "Statistics",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "TotalPaidAmount",
                table: "Statistics",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<double>(
                name: "TotalAmount",
                table: "Statistics",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
