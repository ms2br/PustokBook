using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PustokBook.Migrations
{
    public partial class UpdateAllTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActiveImage",
                table: "ProductImages");

            migrationBuilder.RenameColumn(
                name: "ImagesUrl",
                table: "ProductImages",
                newName: "ImageUrl");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Authors",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Authors");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "ProductImages",
                newName: "ImagesUrl");

            migrationBuilder.AddColumn<string>(
                name: "ActiveImage",
                table: "ProductImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
