using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkSearch.Migrations
{
    /// <inheritdoc />
    public partial class AddedPhotoUrlToEmployers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "photo_url",
                table: "sole_proprietor",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "photo_url",
                table: "company",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "photo_url",
                table: "sole_proprietor");

            migrationBuilder.DropColumn(
                name: "photo_url",
                table: "company");
        }
    }
}
