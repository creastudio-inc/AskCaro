using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AskCaro.Migrations
{
    public partial class relationcategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CategoriescategorieId",
                table: "Conversations",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_CategoriescategorieId",
                table: "Conversations",
                column: "CategoriescategorieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_Categories_CategoriescategorieId",
                table: "Conversations",
                column: "CategoriescategorieId",
                principalTable: "Categories",
                principalColumn: "categorieId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_Categories_CategoriescategorieId",
                table: "Conversations");

            migrationBuilder.DropIndex(
                name: "IX_Conversations_CategoriescategorieId",
                table: "Conversations");

            migrationBuilder.DropColumn(
                name: "CategoriescategorieId",
                table: "Conversations");
        }
    }
}
