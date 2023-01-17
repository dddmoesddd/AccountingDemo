using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountingJournal.Infrastrcture.Migrations
{
    /// <inheritdoc />
    public partial class changetypeofpk2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "VoucherNumber",
                table: "Journal",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "VoucherNumber",
                table: "Journal",
                type: "float",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
