using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Immobilienverwaltung_Backend.Migrations
{
    /// <inheritdoc />
    public partial class updatehausgeldcascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImmobilienOverviews_ImmobilienHausgeld_ImmobilienHausgeldId",
                table: "ImmobilienOverviews");

            migrationBuilder.AddForeignKey(
                name: "FK_ImmobilienOverviews_ImmobilienHausgeld_ImmobilienHausgeldId",
                table: "ImmobilienOverviews",
                column: "ImmobilienHausgeldId",
                principalTable: "ImmobilienHausgeld",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImmobilienOverviews_ImmobilienHausgeld_ImmobilienHausgeldId",
                table: "ImmobilienOverviews");

            migrationBuilder.AddForeignKey(
                name: "FK_ImmobilienOverviews_ImmobilienHausgeld_ImmobilienHausgeldId",
                table: "ImmobilienOverviews",
                column: "ImmobilienHausgeldId",
                principalTable: "ImmobilienHausgeld",
                principalColumn: "Id");
        }
    }
}
