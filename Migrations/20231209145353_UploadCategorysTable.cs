using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PustokBook.Migrations
{
    public partial class UploadCategorysTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryDataId",
                table: "Categorys",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categorys_CategoryDataId",
                table: "Categorys",
                column: "CategoryDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categorys_Categorys_CategoryDataId",
                table: "Categorys",
                column: "CategoryDataId",
                principalTable: "Categorys",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categorys_Categorys_CategoryDataId",
                table: "Categorys");

            migrationBuilder.DropIndex(
                name: "IX_Categorys_CategoryDataId",
                table: "Categorys");

            migrationBuilder.DropColumn(
                name: "CategoryDataId",
                table: "Categorys");
        }
    }
}
