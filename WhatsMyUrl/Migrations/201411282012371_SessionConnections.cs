namespace WhatsMyUrl.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SessionConnections : DbMigration
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
                .PrimaryKey(t => t.CreatedOn).Index(x => x.HubId).Index(x => x.SessionId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SessionConnections");
        }
    }
}
