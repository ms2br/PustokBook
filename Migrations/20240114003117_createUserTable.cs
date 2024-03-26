using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PustokBook.Migrations
{
    public partial class createUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Fullname",
                table: "AspNetUsers",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "ProfileImageUrl",
                table: "AspNetUsers",
                newName: "ProfilActiveImageUrl");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "AspNetUsers",
                newName: "Fullname");

            migrationBuilder.RenameColumn(
                name: "ProfilActiveImageUrl",
                table: "AspNetUsers",
                newName: "ProfileImageUrl");
        }
    }
}
