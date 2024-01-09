using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkSearch.Migrations
{
    /// <inheritdoc />
    public partial class AddedCommonClassToEmployers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_sole_proprietor_user_id",
                table: "sole_proprietor");

            migrationBuilder.DropIndex(
                name: "IX_company_user_id",
                table: "company");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "sole_proprietor",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "sole_proprietor",
                type: "TEXT",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "sole_proprietor",
                type: "VARCHAR(250)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "company",
                type: "VARCHAR(250)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(70)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "company",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "company",
                type: "TEXT",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<DateTime>(
                name: "date_of_birth",
                table: "AspNetUsers",
                type: "DATE",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_sole_proprietor_user_id",
                table: "sole_proprietor",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_company_user_id",
                table: "company",
                column: "user_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_sole_proprietor_user_id",
                table: "sole_proprietor");

            migrationBuilder.DropIndex(
                name: "IX_company_user_id",
                table: "company");

            migrationBuilder.DropColumn(
                name: "description",
                table: "sole_proprietor");

            migrationBuilder.DropColumn(
                name: "name",
                table: "sole_proprietor");

            migrationBuilder.DropColumn(
                name: "description",
                table: "company");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "sole_proprietor",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "company",
                type: "VARCHAR(70)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(250)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "company",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<DateTime>(
                name: "date_of_birth",
                table: "AspNetUsers",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "DATE");

            migrationBuilder.CreateIndex(
                name: "IX_sole_proprietor_user_id",
                table: "sole_proprietor",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_company_user_id",
                table: "company",
                column: "user_id");
        }
    }
}
