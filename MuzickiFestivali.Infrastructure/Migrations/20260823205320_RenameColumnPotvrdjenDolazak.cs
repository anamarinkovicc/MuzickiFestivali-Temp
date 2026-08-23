using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MuzickiFestivali.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameColumnPotvrdjenDolazak : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "potvrdenDolazak",
                table: "Nastupanja",
                newName: "potvrdjenDolazak");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "potvrdjenDolazak",
                table: "Nastupanja",
                newName: "potvrdenDolazak");
        }
    }
}
