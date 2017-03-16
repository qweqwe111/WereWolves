namespace Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class record3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WerewlovesLogs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RoomId = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false, precision: 0),
                        Content = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.WerewlovesLogs");
        }
    }
}
