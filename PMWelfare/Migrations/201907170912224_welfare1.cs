namespace PMWelfare.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class welfare1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MonthlySummaries", "EndDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.MonthlySummaries", "StartAt");
            DropColumn("dbo.MonthlySummaries", "EndAt");
            DropColumn("dbo.MonthlySummaries", "CreatedBy");
            DropColumn("dbo.MonthlySummaries", "CreatedAt");
            DropColumn("dbo.MonthlySummaries", "UpdatedBy");
            DropColumn("dbo.MonthlySummaries", "UpdatedAt");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MonthlySummaries", "UpdatedAt", c => c.DateTime());
            AddColumn("dbo.MonthlySummaries", "UpdatedBy", c => c.String(maxLength: 40, unicode: false));
            AddColumn("dbo.MonthlySummaries", "CreatedAt", c => c.DateTime());
            AddColumn("dbo.MonthlySummaries", "CreatedBy", c => c.String(nullable: false, maxLength: 40, unicode: false));
            AddColumn("dbo.MonthlySummaries", "EndAt", c => c.DateTime());
            AddColumn("dbo.MonthlySummaries", "StartAt", c => c.DateTime());
            DropColumn("dbo.MonthlySummaries", "EndDate");
        }
    }
}
