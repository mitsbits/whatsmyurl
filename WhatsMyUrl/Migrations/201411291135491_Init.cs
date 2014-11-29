namespace WhatsMyUrl.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SessionConnections",
                c => new
                    {
                        CreatedOn = c.DateTime(nullable: false),
                        SessionId = c.String(maxLength: 24),
                        HubId = c.Guid(nullable: false),
                        HubState = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CreatedOn)
                .ForeignKey("dbo.SessionUsers", t => t.SessionId)
                .Index(t => t.SessionId);
            
            CreateTable(
                "dbo.SessionUsers",
                c => new
                    {
                        SessionId = c.String(nullable: false, maxLength: 24),
                        UserName = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.SessionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SessionConnections", "SessionId", "dbo.SessionUsers");
            DropIndex("dbo.SessionConnections", new[] { "SessionId" });
            DropTable("dbo.SessionUsers");
            DropTable("dbo.SessionConnections");
        }
    }
}
