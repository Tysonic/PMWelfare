namespace PMWelfare.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class welfare : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Celebrations", "EventType_Id", "dbo.EventType");
            DropIndex("dbo.Celebrations", new[] { "EventType_Id" });
            RenameColumn(table: "dbo.Celebrations", name: "EventType_Id", newName: "EventTypeId");
            AlterColumn("dbo.Celebrations", "EventTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Celebrations", "EventTypeId");
            AddForeignKey("dbo.Celebrations", "EventTypeId", "dbo.EventType", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Celebrations", "EventTypeId", "dbo.EventType");
            DropIndex("dbo.Celebrations", new[] { "EventTypeId" });
            AlterColumn("dbo.Celebrations", "EventTypeId", c => c.Int());
            RenameColumn(table: "dbo.Celebrations", name: "EventTypeId", newName: "EventType_Id");
            CreateIndex("dbo.Celebrations", "EventType_Id");
            AddForeignKey("dbo.Celebrations", "EventType_Id", "dbo.EventType", "Id");
        }
    }
}
