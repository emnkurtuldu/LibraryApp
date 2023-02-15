using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimApp.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class BookTransactionTableUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "DelayPenaltyFee",
                table: "BookTransactions",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DelayPenaltyFee",
                table: "BookTransactions");
        }
    }
}
