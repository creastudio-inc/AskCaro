using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AskCaro.Migrations
{
    public partial class conversation_categroies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    categorieId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    CreaDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.categorieId);
                });

            migrationBuilder.CreateTable(
                name: "Conversations",
                columns: table => new
                {
                    ConversationsId = table.Column<Guid>(nullable: false),
                    Question = table.Column<string>(nullable: true),
                    HtmlAnswers = table.Column<string>(nullable: true),
                    CreaDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversations", x => x.ConversationsId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Conversations");
        }
    }
}
