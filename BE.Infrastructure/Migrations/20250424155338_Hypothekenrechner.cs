using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BE.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Hypothekenrechner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ImmobilienHypotheken",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Kaufpreis = table.Column<long>(type: "bigint", nullable: false),
                    Kaufnebenkosten_GrunderwerbSteuer_InProzent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Kaufnebenkosten_GrunderwerbSteuer_Betrag = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Kaufnebenkosten_Notarkosten_InProzent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Kaufnebenkosten_Notarkosten_Betrag = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Kaufnebenkosten_Grundbucheintrag_InProzent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Kaufnebenkosten_Grundbucheintrag_Betrag = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Kaufnebenkosten_Maklerprovision_InProzent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Kaufnebenkosten_Maklerprovision_Betrag = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Kaufnebenkosten_Sicherheitspuffer_InProzent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Kaufnebenkosten_Sicherheitspuffer_Betrag = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Kaufnebenkosten_Gesamtnebenkosten_InProzent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Kaufnebenkosten_Gesamtnebenkosten_Betrag = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Eigenkapital_InProzent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Eigenkapital_Betrag = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DarlehensBetrag = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Sollzinsbindung = table.Column<int>(type: "int", nullable: false),
                    Kreditbelastung_Zinsen_InProzent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Kreditbelastung_Zinsen_ProMonat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Kreditbelastung_Zinsen_ProJahr = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Kreditbelastung_Tilgung_InProzent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Kreditbelastung_Tilgung_ProMonat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Kreditbelastung_Tilgung_ProJahr = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Kreditbelastung_Sondertilgung_InProzent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Kreditbelastung_Sondertilgung_ProMonat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Kreditbelastung_Sondertilgung_ProJahr = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Kreditbelastung_GesamtKreditbelastung_InProzent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Kreditbelastung_GesamtKreditbelastung_ProMonat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Kreditbelastung_GesamtKreditbelastung_ProJahr = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Restschuld = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImmobilienOverviewId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImmobilienHypotheken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImmobilienHypotheken_ImmobilienOverviews_ImmobilienOverviewId",
                        column: x => x.ImmobilienOverviewId,
                        principalTable: "ImmobilienOverviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImmobilienHypotheken_ImmobilienOverviewId",
                table: "ImmobilienHypotheken",
                column: "ImmobilienOverviewId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImmobilienHypotheken");
        }
    }
}
