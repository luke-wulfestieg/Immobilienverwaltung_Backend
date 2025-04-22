using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BE.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class hausgeldSubclasses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UmlagefaehigesHausgeldProMonat",
                table: "ImmobilienHausgeld",
                newName: "UmlagefaehigesHausgeld_ProMonat");

            migrationBuilder.RenameColumn(
                name: "UmlagefaehigesHausgeldProJahr",
                table: "ImmobilienHausgeld",
                newName: "UmlagefaehigesHausgeld_ProJahr");

            migrationBuilder.RenameColumn(
                name: "UmlagefaehigesHausgeldInProzent",
                table: "ImmobilienHausgeld",
                newName: "UmlagefaehigesHausgeld_InProzent");

            migrationBuilder.RenameColumn(
                name: "NichtUmlagefaehigesHausgeldProMonat",
                table: "ImmobilienHausgeld",
                newName: "NichtUmlagefaehigesHausgeld_ProMonat");

            migrationBuilder.RenameColumn(
                name: "NichtUmlagefaehigesHausgeldProJahr",
                table: "ImmobilienHausgeld",
                newName: "NichtUmlagefaehigesHausgeld_ProJahr");

            migrationBuilder.RenameColumn(
                name: "NichtUmlagefaehigesHausgeldInProzent",
                table: "ImmobilienHausgeld",
                newName: "NichtUmlagefaehigesHausgeld_InProzent");

            migrationBuilder.RenameColumn(
                name: "HausgeldProQuadratmeter",
                table: "ImmobilienHausgeld",
                newName: "Hausgeld_ProQuadratmeter");

            migrationBuilder.RenameColumn(
                name: "HausgeldProMonat",
                table: "ImmobilienHausgeld",
                newName: "Hausgeld_ProMonat");

            migrationBuilder.RenameColumn(
                name: "HausgeldProJahr",
                table: "ImmobilienHausgeld",
                newName: "Hausgeld_ProJahr");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UmlagefaehigesHausgeld_ProMonat",
                table: "ImmobilienHausgeld",
                newName: "UmlagefaehigesHausgeldProMonat");

            migrationBuilder.RenameColumn(
                name: "UmlagefaehigesHausgeld_ProJahr",
                table: "ImmobilienHausgeld",
                newName: "UmlagefaehigesHausgeldProJahr");

            migrationBuilder.RenameColumn(
                name: "UmlagefaehigesHausgeld_InProzent",
                table: "ImmobilienHausgeld",
                newName: "UmlagefaehigesHausgeldInProzent");

            migrationBuilder.RenameColumn(
                name: "NichtUmlagefaehigesHausgeld_ProMonat",
                table: "ImmobilienHausgeld",
                newName: "NichtUmlagefaehigesHausgeldProMonat");

            migrationBuilder.RenameColumn(
                name: "NichtUmlagefaehigesHausgeld_ProJahr",
                table: "ImmobilienHausgeld",
                newName: "NichtUmlagefaehigesHausgeldProJahr");

            migrationBuilder.RenameColumn(
                name: "NichtUmlagefaehigesHausgeld_InProzent",
                table: "ImmobilienHausgeld",
                newName: "NichtUmlagefaehigesHausgeldInProzent");

            migrationBuilder.RenameColumn(
                name: "Hausgeld_ProQuadratmeter",
                table: "ImmobilienHausgeld",
                newName: "HausgeldProQuadratmeter");

            migrationBuilder.RenameColumn(
                name: "Hausgeld_ProMonat",
                table: "ImmobilienHausgeld",
                newName: "HausgeldProMonat");

            migrationBuilder.RenameColumn(
                name: "Hausgeld_ProJahr",
                table: "ImmobilienHausgeld",
                newName: "HausgeldProJahr");
        }
    }
}
