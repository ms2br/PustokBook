using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PustokBook.Migrations
{
    public partial class UpdateAuthorBookTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AuthorBooks",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AuthorBooks");
        }
    }
}
