namespace AskCaro_Web_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Conversations", "Categories_categorieId", "dbo.Categories");
            DropForeignKey("dbo.Answer", "QuestionModel_QuestionId", "dbo.Question");
            DropPrimaryKey("dbo.Answer");
            DropPrimaryKey("dbo.Categories");
            DropPrimaryKey("dbo.Conversations");
            DropPrimaryKey("dbo.Question");
            DropPrimaryKey("dbo.Tags");
            AlterColumn("dbo.Answer", "AnswerId", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.Categories", "categorieId", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.Conversations", "ConversationsId", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.Question", "QuestionId", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.Tags", "TagId", c => c.Guid(nullable: false, identity: true));
            AddPrimaryKey("dbo.Answer", "AnswerId");
            AddPrimaryKey("dbo.Categories", "categorieId");
            AddPrimaryKey("dbo.Conversations", "ConversationsId");
            AddPrimaryKey("dbo.Question", "QuestionId");
            AddPrimaryKey("dbo.Tags", "TagId");
            AddForeignKey("dbo.Conversations", "Categories_categorieId", "dbo.Categories", "categorieId");
            AddForeignKey("dbo.Answer", "QuestionModel_QuestionId", "dbo.Question", "QuestionId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Answer", "QuestionModel_QuestionId", "dbo.Question");
            DropForeignKey("dbo.Conversations", "Categories_categorieId", "dbo.Categories");
            DropPrimaryKey("dbo.Tags");
            DropPrimaryKey("dbo.Question");
            DropPrimaryKey("dbo.Conversations");
            DropPrimaryKey("dbo.Categories");
            DropPrimaryKey("dbo.Answer");
            AlterColumn("dbo.Tags", "TagId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Question", "QuestionId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Conversations", "ConversationsId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Categories", "categorieId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Answer", "AnswerId", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.Tags", "TagId");
            AddPrimaryKey("dbo.Question", "QuestionId");
            AddPrimaryKey("dbo.Conversations", "ConversationsId");
            AddPrimaryKey("dbo.Categories", "categorieId");
            AddPrimaryKey("dbo.Answer", "AnswerId");
            AddForeignKey("dbo.Answer", "QuestionModel_QuestionId", "dbo.Question", "QuestionId");
            AddForeignKey("dbo.Conversations", "Categories_categorieId", "dbo.Categories", "categorieId");
        }
    }
}
