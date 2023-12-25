using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PustokBook.Migrations
{
    public partial class updateProductTablesColumnName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProducTags_Products_ProductId",
                table: "ProducTags");

            migrationBuilder.DropColumn(
                name: "ProducId",
                table: "ProducTags");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ProducTags",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProducTags_Products_ProductId",
                table: "ProducTags",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProducTags_Products_ProductId",
                table: "ProducTags");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ProducTags",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ProducId",
                table: "ProducTags",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ProducTags_Products_ProductId",
                table: "ProducTags",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
