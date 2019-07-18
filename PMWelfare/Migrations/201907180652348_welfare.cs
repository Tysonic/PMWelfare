namespace PMWelfare.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class welfare : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Deposits", "Amount", c => c.Decimal(nullable: false, storeType: "money"));
            AlterColumn("dbo.Subscriptions", "Amount", c => c.Decimal(nullable: false, storeType: "money"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Subscriptions", "Amount", c => c.Decimal(storeType: "money"));
            AlterColumn("dbo.Deposits", "Amount", c => c.Decimal(storeType: "money"));
        }
    }
}
