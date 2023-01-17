using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountingVoucher.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initialdatabse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Voucher",
                columns: table => new
                {
                    VoucherNumber = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsBalance = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voucher", x => x.VoucherNumber);
                });

            migrationBuilder.CreateTable(
                name: "VoucherItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreditorPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DebtorPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    StreetPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    VoucherType = table.Column<int>(type: "int", nullable: false),
                    VoucherNumber = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoucherItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VoucherItems_Voucher_VoucherNumber",
                        column: x => x.VoucherNumber,
                        principalTable: "Voucher",
                        principalColumn: "VoucherNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VoucherItems_VoucherNumber",
                table: "VoucherItems",
                column: "VoucherNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VoucherItems");

            migrationBuilder.DropTable(
                name: "Voucher");
        }
    }
}
