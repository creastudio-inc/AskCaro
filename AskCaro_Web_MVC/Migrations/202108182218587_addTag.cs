namespace AskCaro_Web_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTag : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Question", "Tag", c => c.String());
            DropColumn("dbo.Question", "SiteClone");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Question", "SiteClone", c => c.String());
            DropColumn("dbo.Question", "Tag");
        }
    }
}
