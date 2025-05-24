using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BE.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Bruttomietrendite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BruttoMietrenditen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Kaufpreis = table.Column<long>(type: "bigint", nullable: false),
                    Wohnflaeche = table.Column<double>(type: "float", nullable: false),
                    UmlagefaehigesHausgeld_InProzent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UmlagefaehigesHausgeld_ProMonat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UmlagefaehigesHausgeld_ProJahr = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Kaltmiete_ProQuadratmeter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Kaltmiete_ProMonat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Kaltmiete_ProJahr = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Warmmiete_ProQuadratmeter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Warmmiete_ProMonat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Warmmiete_ProJahr = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    KaufpreisFaktor = table.Column<double>(type: "float", nullable: false),
                    BruttoMietrendite = table.Column<double>(type: "float", nullable: false),
                    ImmobilienOverviewId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BruttoMietrenditen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BruttoMietrenditen_ImmobilienOverviews_ImmobilienOverviewId",
                        column: x => x.ImmobilienOverviewId,
                        principalTable: "ImmobilienOverviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BruttoMietrenditen_ImmobilienOverviewId",
                table: "BruttoMietrenditen",
                column: "ImmobilienOverviewId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BruttoMietrenditen");
        }
    }
}
