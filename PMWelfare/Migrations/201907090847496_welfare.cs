namespace PMWelfare.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class welfare : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Subscriptions", "Amount", c => c.Decimal(storeType: "money"));
            AlterColumn("dbo.Subscriptions", "SubMonth", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Subscriptions", "SubMonth", c => c.Byte(nullable: false));
            AlterColumn("dbo.Subscriptions", "Amount", c => c.Decimal(nullable: false, storeType: "money"));
        }
    }
}
