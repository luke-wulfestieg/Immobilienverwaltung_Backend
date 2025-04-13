using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Immobilienverwaltung_Backend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ImmobilienHausgeld",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hausgeld_proQuadratmeter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Hausgeld_proMonat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Hausgeld_proJahr = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Umlagefaehiges_Hausgeld_inProzent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Umlagefaehiges_Hausgeld_proMonat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Umlagefaehiges_Hausgeld_proJahr = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Nicht_Umlagefaehiges_Hausgeld_inProzent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Nicht_Umlagefaehiges_Hausgeld_proMonat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Nicht_Umlagefaehiges_Hausgeld_proJahr = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImmobilienHausgeld", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ImmobilienTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImmobilienType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImmobilienTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ImmobilienOverviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImmobilienName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImmobilienTypeId = table.Column<int>(type: "int", nullable: false),
                    Kaufpreis = table.Column<long>(type: "bigint", nullable: false),
                    ZimmerAnzahl = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Wohnflaeche = table.Column<double>(type: "float", nullable: false),
                    BruttoMietRendite = table.Column<double>(type: "float", nullable: false),
                    ImmobilienUeberschuss = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImmobilienHausgeldId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImmobilienOverviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImmobilienOverviews_ImmobilienHausgeld_ImmobilienHausgeldId",
                        column: x => x.ImmobilienHausgeldId,
                        principalTable: "ImmobilienHausgeld",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ImmobilienOverviews_ImmobilienTypes_ImmobilienTypeId",
                        column: x => x.ImmobilienTypeId,
                        principalTable: "ImmobilienTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImmobilienOverviews_ImmobilienHausgeldId",
                table: "ImmobilienOverviews",
                column: "ImmobilienHausgeldId",
                unique: true,
                filter: "[ImmobilienHausgeldId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ImmobilienOverviews_ImmobilienTypeId",
                table: "ImmobilienOverviews",
                column: "ImmobilienTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImmobilienOverviews");

            migrationBuilder.DropTable(
                name: "ImmobilienHausgeld");

            migrationBuilder.DropTable(
                name: "ImmobilienTypes");
        }
    }
}
