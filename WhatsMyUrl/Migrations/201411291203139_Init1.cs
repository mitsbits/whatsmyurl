namespace WhatsMyUrl.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SessionConnections", "SessionId", "dbo.SessionUsers");
            DropIndex("dbo.SessionConnections", new[] { "SessionId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.SessionConnections", "SessionId");
            AddForeignKey("dbo.SessionConnections", "SessionId", "dbo.SessionUsers", "SessionId");
        }
    }
}
