using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MuzickiFestivali.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddImageUrlToFestival : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "xKoordinata",
                table: "Bine");

            migrationBuilder.DropColumn(
                name: "yKoordinata",
                table: "Bine");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "Osobe",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "SlikaUrl",
                table: "Festivali",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "naziv",
                table: "Bine",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Osobe_email",
                table: "Osobe",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Osobe_email",
                table: "Osobe");

            migrationBuilder.DropColumn(
                name: "SlikaUrl",
                table: "Festivali");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "Osobe",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "naziv",
                table: "Bine",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<float>(
                name: "xKoordinata",
                table: "Bine",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "yKoordinata",
                table: "Bine",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
