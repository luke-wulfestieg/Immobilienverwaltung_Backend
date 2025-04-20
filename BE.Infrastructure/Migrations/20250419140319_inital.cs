using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BE.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class inital : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ImmobilienTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    ImmobilienUeberschuss = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImmobilienOverviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImmobilienOverviews_ImmobilienTypes_ImmobilienTypeId",
                        column: x => x.ImmobilienTypeId,
                        principalTable: "ImmobilienTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ImmobilienHausgeld",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HausgeldProQuadratmeter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HausgeldProMonat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HausgeldProJahr = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UmlagefaehigesHausgeldInProzent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UmlagefaehigesHausgeldProMonat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UmlagefaehigesHausgeldProJahr = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NichtUmlagefaehigesHausgeldInProzent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NichtUmlagefaehigesHausgeldProMonat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NichtUmlagefaehigesHausgeldProJahr = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImmobilienOverviewId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImmobilienHausgeld", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImmobilienHausgeld_ImmobilienOverviews_ImmobilienOverviewId",
                        column: x => x.ImmobilienOverviewId,
                        principalTable: "ImmobilienOverviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImmobilienHausgeld_ImmobilienOverviewId",
                table: "ImmobilienHausgeld",
                column: "ImmobilienOverviewId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ImmobilienOverviews_ImmobilienTypeId",
                table: "ImmobilienOverviews",
                column: "ImmobilienTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImmobilienHausgeld");

            migrationBuilder.DropTable(
                name: "ImmobilienOverviews");

            migrationBuilder.DropTable(
                name: "ImmobilienTypes");
        }
    }
}
