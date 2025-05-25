using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BE.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removeBruttomietrenditeProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BruttoMietRendite",
                table: "ImmobilienOverviews");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "BruttoMietRendite",
                table: "ImmobilienOverviews",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
