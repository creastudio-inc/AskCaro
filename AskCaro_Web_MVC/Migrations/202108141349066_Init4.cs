namespace AskCaro_Web_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Answer", "QuestionModel_QuestionId", "dbo.Question");
            DropIndex("dbo.Answer", new[] { "QuestionModel_QuestionId" });
            RenameColumn(table: "dbo.Answer", name: "QuestionModel_QuestionId", newName: "QuestionId");
            DropPrimaryKey("dbo.Answer");
            DropPrimaryKey("dbo.Conversations");
            DropPrimaryKey("dbo.Question");
            AddColumn("dbo.Answer", "Id", c => c.Guid(nullable: false, identity: true));
            AddColumn("dbo.Conversations", "Id", c => c.Guid(nullable: false, identity: true));
            AddColumn("dbo.Question", "Id", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.Answer", "QuestionId", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.Answer", "Id");
            AddPrimaryKey("dbo.Conversations", "Id");
            AddPrimaryKey("dbo.Question", "Id");
            CreateIndex("dbo.Answer", "QuestionId");
            AddForeignKey("dbo.Answer", "QuestionId", "dbo.Question", "Id", cascadeDelete: true);
            DropColumn("dbo.Answer", "AnswerId");
            DropColumn("dbo.Conversations", "ConversationsId");
            DropColumn("dbo.Question", "QuestionId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Question", "QuestionId", c => c.Guid(nullable: false, identity: true));
            AddColumn("dbo.Conversations", "ConversationsId", c => c.Guid(nullable: false, identity: true));
            AddColumn("dbo.Answer", "AnswerId", c => c.Guid(nullable: false, identity: true));
            DropForeignKey("dbo.Answer", "QuestionId", "dbo.Question");
            DropIndex("dbo.Answer", new[] { "QuestionId" });
            DropPrimaryKey("dbo.Question");
            DropPrimaryKey("dbo.Conversations");
            DropPrimaryKey("dbo.Answer");
            AlterColumn("dbo.Answer", "QuestionId", c => c.Guid());
            DropColumn("dbo.Question", "Id");
            DropColumn("dbo.Conversations", "Id");
            DropColumn("dbo.Answer", "Id");
            AddPrimaryKey("dbo.Question", "QuestionId");
            AddPrimaryKey("dbo.Conversations", "ConversationsId");
            AddPrimaryKey("dbo.Answer", "AnswerId");
            RenameColumn(table: "dbo.Answer", name: "QuestionId", newName: "QuestionModel_QuestionId");
            CreateIndex("dbo.Answer", "QuestionModel_QuestionId");
            AddForeignKey("dbo.Answer", "QuestionModel_QuestionId", "dbo.Question", "QuestionId");
        }
    }
}
