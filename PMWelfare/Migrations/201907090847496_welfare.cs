namespace PMWelfare.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class welfare : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Subscriptions", "Amount", c => c.Decimal(storeType: "money"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Subscriptions", "Amount", c => c.Decimal(nullable: false, storeType: "money"));
        }
    }
}
