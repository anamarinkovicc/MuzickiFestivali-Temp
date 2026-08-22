using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MuzickiFestivali.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bine",
                columns: table => new
                {
                    idBina = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    kapacitet = table.Column<int>(type: "int", nullable: false),
                    xKoordinata = table.Column<float>(type: "real", nullable: false),
                    yKoordinata = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bine", x => x.idBina);
                });

            migrationBuilder.CreateTable(
                name: "Osobe",
                columns: table => new
                {
                    idOsoba = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    prezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lozinka = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Osobe", x => x.idOsoba);
                });

            migrationBuilder.CreateTable(
                name: "Izvodjaci",
                columns: table => new
                {
                    idOsoba = table.Column<int>(type: "int", nullable: false),
                    umetnickoIme = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    biografija = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    zanr = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Izvodjaci", x => x.idOsoba);
                    table.ForeignKey(
                        name: "FK_Izvodjaci_Osobe_idOsoba",
                        column: x => x.idOsoba,
                        principalTable: "Osobe",
                        principalColumn: "idOsoba",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Korisnici",
                columns: table => new
                {
                    idOsoba = table.Column<int>(type: "int", nullable: false),
                    omiljeniZanr = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnici", x => x.idOsoba);
                    table.ForeignKey(
                        name: "FK_Korisnici_Osobe_idOsoba",
                        column: x => x.idOsoba,
                        principalTable: "Osobe",
                        principalColumn: "idOsoba",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Zaposleni",
                columns: table => new
                {
                    idOsoba = table.Column<int>(type: "int", nullable: false),
                    pozicija = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zaposleni", x => x.idOsoba);
                    table.ForeignKey(
                        name: "FK_Zaposleni_Osobe_idOsoba",
                        column: x => x.idOsoba,
                        principalTable: "Osobe",
                        principalColumn: "idOsoba",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Festivali",
                columns: table => new
                {
                    idFestival = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    opis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    datumPocetka = table.Column<DateTime>(type: "datetime2", nullable: false),
                    datumZavrsetka = table.Column<DateTime>(type: "datetime2", nullable: false),
                    kapacitet = table.Column<int>(type: "int", nullable: false),
                    idOsoba = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Festivali", x => x.idFestival);
                    table.ForeignKey(
                        name: "FK_Festivali_Zaposleni_idOsoba",
                        column: x => x.idOsoba,
                        principalTable: "Zaposleni",
                        principalColumn: "idOsoba");
                });

            migrationBuilder.CreateTable(
                name: "Nastupi",
                columns: table => new
                {
                    idNastup = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idFestival = table.Column<int>(type: "int", nullable: false),
                    naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    opis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    zanr = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nastupi", x => new { x.idFestival, x.idNastup });
                    table.ForeignKey(
                        name: "FK_Nastupi_Festivali_idFestival",
                        column: x => x.idFestival,
                        principalTable: "Festivali",
                        principalColumn: "idFestival",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lajkovi",
                columns: table => new
                {
                    idOsoba = table.Column<int>(type: "int", nullable: false),
                    idNastup = table.Column<int>(type: "int", nullable: false),
                    idFestival = table.Column<int>(type: "int", nullable: false),
                    datumVremeLajka = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lajkovi", x => new { x.idOsoba, x.idFestival, x.idNastup });
                    table.ForeignKey(
                        name: "FK_Lajkovi_Korisnici_idOsoba",
                        column: x => x.idOsoba,
                        principalTable: "Korisnici",
                        principalColumn: "idOsoba");
                    table.ForeignKey(
                        name: "FK_Lajkovi_Nastupi_idFestival_idNastup",
                        columns: x => new { x.idFestival, x.idNastup },
                        principalTable: "Nastupi",
                        principalColumns: new[] { "idFestival", "idNastup" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Termini",
                columns: table => new
                {
                    idTermin = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idNastup = table.Column<int>(type: "int", nullable: false),
                    idFestival = table.Column<int>(type: "int", nullable: false),
                    tip = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    vremePocetka = table.Column<DateTime>(type: "datetime2", nullable: false),
                    vremeZavrsetka = table.Column<DateTime>(type: "datetime2", nullable: false),
                    napomena = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    idBina = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Termini", x => new { x.idFestival, x.idNastup, x.idTermin });
                    table.ForeignKey(
                        name: "FK_Termini_Bine_idBina",
                        column: x => x.idBina,
                        principalTable: "Bine",
                        principalColumn: "idBina");
                    table.ForeignKey(
                        name: "FK_Termini_Nastupi_idFestival_idNastup",
                        columns: x => new { x.idFestival, x.idNastup },
                        principalTable: "Nastupi",
                        principalColumns: new[] { "idFestival", "idNastup" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Nastupanja",
                columns: table => new
                {
                    idOsoba = table.Column<int>(type: "int", nullable: false),
                    idTermin = table.Column<int>(type: "int", nullable: false),
                    idFestival = table.Column<int>(type: "int", nullable: false),
                    idNastup = table.Column<int>(type: "int", nullable: false),
                    uloga = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    potvrdenDolazak = table.Column<bool>(type: "bit", nullable: false),
                    napomena = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nastupanja", x => new { x.idOsoba, x.idFestival, x.idNastup, x.idTermin });
                    table.ForeignKey(
                        name: "FK_Nastupanja_Izvodjaci_idOsoba",
                        column: x => x.idOsoba,
                        principalTable: "Izvodjaci",
                        principalColumn: "idOsoba");
                    table.ForeignKey(
                        name: "FK_Nastupanja_Termini_idFestival_idNastup_idTermin",
                        columns: x => new { x.idFestival, x.idNastup, x.idTermin },
                        principalTable: "Termini",
                        principalColumns: new[] { "idFestival", "idNastup", "idTermin" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Festivali_idOsoba",
                table: "Festivali",
                column: "idOsoba");

            migrationBuilder.CreateIndex(
                name: "IX_Lajkovi_idFestival_idNastup",
                table: "Lajkovi",
                columns: new[] { "idFestival", "idNastup" });

            migrationBuilder.CreateIndex(
                name: "IX_Nastupanja_idFestival_idNastup_idTermin",
                table: "Nastupanja",
                columns: new[] { "idFestival", "idNastup", "idTermin" });

            migrationBuilder.CreateIndex(
                name: "IX_Termini_idBina",
                table: "Termini",
                column: "idBina");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lajkovi");

            migrationBuilder.DropTable(
                name: "Nastupanja");

            migrationBuilder.DropTable(
                name: "Korisnici");

            migrationBuilder.DropTable(
                name: "Izvodjaci");

            migrationBuilder.DropTable(
                name: "Termini");

            migrationBuilder.DropTable(
                name: "Bine");

            migrationBuilder.DropTable(
                name: "Nastupi");

            migrationBuilder.DropTable(
                name: "Festivali");

            migrationBuilder.DropTable(
                name: "Zaposleni");

            migrationBuilder.DropTable(
                name: "Osobe");
        }
    }
}
