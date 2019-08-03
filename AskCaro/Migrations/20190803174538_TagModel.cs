using Microsoft.EntityFrameworkCore.Migrations;

namespace AskCaro.Migrations
{
    public partial class TagModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Tags",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaginationActuel",
                table: "Tags",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PaginationFound",
                table: "Tags",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "PaginationActuel",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "PaginationFound",
                table: "Tags");
        }
    }
}
