using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Immobilienverwaltung_Backend.Migrations
{
    /// <inheritdoc />
    public partial class haushaltoverviewID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ImmobilienOverviewId",
                table: "ImmobilienHausgeld",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImmobilienOverviewId",
                table: "ImmobilienHausgeld");
        }
    }
}
