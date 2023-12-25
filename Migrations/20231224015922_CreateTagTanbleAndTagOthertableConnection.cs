using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PustokBook.Migrations
{
    public partial class CreateTagTanbleAndTagOthertableConnection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProducTag_Products_ProductId",
                table: "ProducTag");

            migrationBuilder.DropForeignKey(
                name: "FK_ProducTag_Tag_TagId",
                table: "ProducTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tag",
                table: "Tag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProducTag",
                table: "ProducTag");

            migrationBuilder.RenameTable(
                name: "Tag",
                newName: "Tags");

            migrationBuilder.RenameTable(
                name: "ProducTag",
                newName: "ProducTags");

            migrationBuilder.RenameIndex(
                name: "IX_ProducTag_TagId",
                table: "ProducTags",
                newName: "IX_ProducTags_TagId");

            migrationBuilder.RenameIndex(
                name: "IX_ProducTag_ProductId",
                table: "ProducTags",
                newName: "IX_ProducTags_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tags",
                table: "Tags",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProducTags",
                table: "ProducTags",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProducTags_Products_ProductId",
                table: "ProducTags",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProducTags_Tags_TagId",
                table: "ProducTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProducTags_Products_ProductId",
                table: "ProducTags");

            migrationBuilder.DropForeignKey(
                name: "FK_ProducTags_Tags_TagId",
                table: "ProducTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tags",
                table: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProducTags",
                table: "ProducTags");

            migrationBuilder.RenameTable(
                name: "Tags",
                newName: "Tag");

            migrationBuilder.RenameTable(
                name: "ProducTags",
                newName: "ProducTag");

            migrationBuilder.RenameIndex(
                name: "IX_ProducTags_TagId",
                table: "ProducTag",
                newName: "IX_ProducTag_TagId");

            migrationBuilder.RenameIndex(
                name: "IX_ProducTags_ProductId",
                table: "ProducTag",
                newName: "IX_ProducTag_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tag",
                table: "Tag",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProducTag",
                table: "ProducTag",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProducTag_Products_ProductId",
                table: "ProducTag",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProducTag_Tag_TagId",
                table: "ProducTag",
                column: "TagId",
                principalTable: "Tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
