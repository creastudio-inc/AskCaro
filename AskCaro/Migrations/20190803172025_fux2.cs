using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AskCaro.Migrations
{
    public partial class fux2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TagsQuestions",
                columns: table => new
                {
                    TagQuestionId = table.Column<Guid>(nullable: false),
                    TagId = table.Column<Guid>(nullable: false),
                    QuestionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagsQuestions", x => x.TagQuestionId);
                    table.ForeignKey(
                        name: "FK_TagsQuestions_Question_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagsQuestions_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "TagId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TagsQuestions_QuestionId",
                table: "TagsQuestions",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_TagsQuestions_TagId",
                table: "TagsQuestions",
                column: "TagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TagsQuestions");
        }
    }
}
