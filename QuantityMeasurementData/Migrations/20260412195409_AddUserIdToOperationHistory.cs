using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuantityMeasurementData.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdToOperationHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Operations",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Operations");
        }
    }
}
