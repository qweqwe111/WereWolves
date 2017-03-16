namespace Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class record1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rooms", "Createtime", c => c.DateTime(nullable: false, precision: 0));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Rooms", "Createtime");
        }
    }
}
