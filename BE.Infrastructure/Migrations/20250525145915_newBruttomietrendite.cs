using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BE.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class newBruttomietrendite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BruttoMietrenditen_ImmobilienOverviews_ImmobilienOverviewId",
                table: "BruttoMietrenditen");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BruttoMietrenditen",
                table: "BruttoMietrenditen");

            migrationBuilder.RenameTable(
                name: "BruttoMietrenditen",
                newName: "Bruttomietrenditen");

            migrationBuilder.RenameColumn(
                name: "BruttoMietrendite",
                table: "Bruttomietrenditen",
                newName: "BruttomietrenditeBetrag");

            migrationBuilder.RenameIndex(
                name: "IX_BruttoMietrenditen_ImmobilienOverviewId",
                table: "Bruttomietrenditen",
                newName: "IX_Bruttomietrenditen_ImmobilienOverviewId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bruttomietrenditen",
                table: "Bruttomietrenditen",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bruttomietrenditen_ImmobilienOverviews_ImmobilienOverviewId",
                table: "Bruttomietrenditen",
                column: "ImmobilienOverviewId",
                principalTable: "ImmobilienOverviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bruttomietrenditen_ImmobilienOverviews_ImmobilienOverviewId",
                table: "Bruttomietrenditen");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bruttomietrenditen",
                table: "Bruttomietrenditen");

            migrationBuilder.RenameTable(
                name: "Bruttomietrenditen",
                newName: "BruttoMietrenditen");

            migrationBuilder.RenameColumn(
                name: "BruttomietrenditeBetrag",
                table: "BruttoMietrenditen",
                newName: "BruttoMietrendite");

            migrationBuilder.RenameIndex(
                name: "IX_Bruttomietrenditen_ImmobilienOverviewId",
                table: "BruttoMietrenditen",
                newName: "IX_BruttoMietrenditen_ImmobilienOverviewId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BruttoMietrenditen",
                table: "BruttoMietrenditen",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BruttoMietrenditen_ImmobilienOverviews_ImmobilienOverviewId",
                table: "BruttoMietrenditen",
                column: "ImmobilienOverviewId",
                principalTable: "ImmobilienOverviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
