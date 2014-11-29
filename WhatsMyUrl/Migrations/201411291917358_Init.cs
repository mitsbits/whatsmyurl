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
                .PrimaryKey(t => t.CreatedOn);
            
            CreateTable(
                "dbo.SessionGroupMessages",
                c => new
                    {
                        CreatedOn = c.DateTime(nullable: false),
                        InternalRecipients = c.String(),
                        Sender = c.String(),
                        Body = c.String(),
                    })
                .PrimaryKey(t => t.CreatedOn);
            
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
            DropTable("dbo.SessionUsers");
            DropTable("dbo.SessionGroupMessages");
            DropTable("dbo.SessionConnections");
        }
    }
}
