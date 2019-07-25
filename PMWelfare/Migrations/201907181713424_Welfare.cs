namespace PMWelfare.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Welfare : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Subscriptions", "SubMonth", c => c.Int(nullable: false));
            AlterColumn("dbo.Subscriptions", "SubYear", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Subscriptions", "SubYear", c => c.Int());
            AlterColumn("dbo.Subscriptions", "SubMonth", c => c.Int());
        }
    }
}
