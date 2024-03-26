using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PustokBook.Migrations
{
    public partial class createRolesTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4b4459a2-3f35-43c5-b4cb-55cc1786dec8", "2cb581da-c617-4fb8-803e-046763888c99", "Moderator", "MODERATOR" },
                    { "606bb41b-dddd-403f-859c-04f9974146bb", "0c54d82b-f591-4c4b-b547-bffd757d02eb", "Member", "MEMBER" },
                    { "e4cd4bfe-1c44-4760-86a0-6ce037bb16e6", "0e826a9c-2d0e-4a35-a9d6-9ffb87b2b81a", "Admin", "ADMIN" },
                    { "f29df050-89e5-4a4e-bf1e-645642db05e2", "71e36bf3-777a-4d15-b585-d1af08be730a", "SuperAdmin", "SUPERADMIN" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4b4459a2-3f35-43c5-b4cb-55cc1786dec8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "606bb41b-dddd-403f-859c-04f9974146bb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e4cd4bfe-1c44-4760-86a0-6ce037bb16e6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f29df050-89e5-4a4e-bf1e-645642db05e2");
        }
    }
}
