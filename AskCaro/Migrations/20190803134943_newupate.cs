using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AskCaro.Migrations
{
    public partial class newupate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SiteClone",
                table: "Question",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    TagId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    SiteClone = table.Column<string>(nullable: true),
                    CreaDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.TagId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropColumn(
                name: "SiteClone",
                table: "Question");
        }
    }
}
