using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BE.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addgesamtbelastungfeature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gesamtbelastungen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Kreditbelastung_ProMonat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Kreditbelastung_ProJahr = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Ruecklagen_ProMonat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Ruecklagen_ProJahr = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NichtUmlagefaehigesHausgeld_ProMonat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NichtUmlagefaehigesHausgeld_ProJahr = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GesamtbelastungBetrag_ProMonat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GesamtbelastungBetrag_ProJahr = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImmobilienOverviewId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gesamtbelastungen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gesamtbelastungen_ImmobilienOverviews_ImmobilienOverviewId",
                        column: x => x.ImmobilienOverviewId,
                        principalTable: "ImmobilienOverviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Gesamtbelastungen_ImmobilienOverviewId",
                table: "Gesamtbelastungen",
                column: "ImmobilienOverviewId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Gesamtbelastungen");
        }
    }
}
