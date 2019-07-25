namespace PMWelfare.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Welfare : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActivityLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(maxLength: 40, unicode: false),
                        DeviceIp = c.String(nullable: false, maxLength: 20, unicode: false),
                        DeviceMac = c.String(nullable: false, maxLength: 40, unicode: false),
                        AcitivityTime = c.DateTime(),
                        Activity = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Members", t => t.UserName)
                .Index(t => t.UserName);
            
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        UserName = c.String(nullable: false, maxLength: 40, unicode: false),
                        FirstName = c.String(maxLength: 40, unicode: false),
                        OtherName = c.String(maxLength: 40, unicode: false),
                        Email = c.String(nullable: false, maxLength: 40, unicode: false),
                        DOB = c.DateTime(storeType: "date"),
                        MemberStatus = c.Int(),
                        IsAdmin = c.Boolean(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 40, unicode: false),
                        CreatedAt = c.DateTime(),
                        TimeStamp = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 40, unicode: false),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.UserName)
                .ForeignKey("dbo.MemberStatus", t => t.MemberStatus)
                .Index(t => t.MemberStatus);
            
            CreateTable(
                "dbo.ChatRooms",
                c => new
                    {
                        ChatId = c.Int(nullable: false, identity: true),
                        UserName = c.String(maxLength: 40, unicode: false),
                        Message = c.String(nullable: false, maxLength: 250, unicode: false),
                        PostedAt = c.DateTime(),
                        TimeStamp = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 40, unicode: false),
                        ParentId = c.Int(nullable: false),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.ChatId)
                .ForeignKey("dbo.Members", t => t.UserName)
                .Index(t => t.UserName);
            
            CreateTable(
                "dbo.Deposits",
                c => new
                    {
                        DepositId = c.Int(nullable: false, identity: true),
                        UserName = c.String(maxLength: 40, unicode: false),
                        Amount = c.Decimal(nullable: false, storeType: "money"),
                        CreatedBy = c.String(nullable: false, maxLength: 40, unicode: false),
                        CreatedAt = c.DateTime(),
                        TimeStamp = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 40, unicode: false),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.DepositId)
                .ForeignKey("dbo.Members", t => t.UserName)
                .Index(t => t.UserName);
            
            CreateTable(
                "dbo.MemberStatus",
                c => new
                    {
                        MembersStatusID = c.Int(nullable: false, identity: true),
                        MemberStatus = c.String(nullable: false, maxLength: 20, unicode: false),
                        CreatedBy = c.String(nullable: false, maxLength: 40, unicode: false),
                        CreatedAt = c.DateTime(),
                        TimeStamp = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 40, unicode: false),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.MembersStatusID);
            
            CreateTable(
                "dbo.Subscriptions",
                c => new
                    {
                        SubId = c.Int(nullable: false, identity: true),
                        UserName = c.String(maxLength: 40, unicode: false),
                        Amount = c.Decimal(nullable: false, storeType: "money"),
                        SubMonth = c.Int(nullable: false),
                        SubYear = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SubId)
                .ForeignKey("dbo.Members", t => t.UserName)
                .Index(t => t.UserName);
            
            CreateTable(
                "dbo.Celebrants",
                c => new
                    {
                        CelebId = c.Int(nullable: false, identity: true),
                        UserName = c.String(maxLength: 40, unicode: false),
                        EventId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CelebId)
                .ForeignKey("dbo.Celebrations", t => t.EventId, cascadeDelete: true)
                .ForeignKey("dbo.Members", t => t.UserName)
                .Index(t => t.UserName)
                .Index(t => t.EventId);
            
            CreateTable(
                "dbo.Celebrations",
                c => new
                    {
                        EventId = c.Int(nullable: false, identity: true),
                        EventName = c.String(nullable: false, maxLength: 40),
                        EventTypeId = c.Int(nullable: false),
                        EventDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 40, unicode: false),
                        CreatedAt = c.DateTime(),
                        TimeStamp = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 40, unicode: false),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.EventId)
                .ForeignKey("dbo.EventType", t => t.EventTypeId, cascadeDelete: true)
                .Index(t => t.EventTypeId);
            
            CreateTable(
                "dbo.EventType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(maxLength: 40),
                        CreatedBy = c.String(nullable: false, maxLength: 40),
                        CreatedAt = c.DateTime(),
                        TimeStamp = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 40),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SupProducts",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        ProductName = c.String(nullable: false, maxLength: 40, unicode: false),
                        UnitPrice = c.Decimal(nullable: false, storeType: "money"),
                        EventId = c.Int(),
                        SupId = c.Int(),
                        CreatedBy = c.String(nullable: false, maxLength: 40, unicode: false),
                        CreatedAt = c.DateTime(),
                        TimeStamp = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 40, unicode: false),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Celebrations", t => t.EventId)
                .ForeignKey("dbo.Suppliers", t => t.SupId)
                .Index(t => t.EventId)
                .Index(t => t.SupId);
            
            CreateTable(
                "dbo.expenses",
                c => new
                    {
                        ExpenseId = c.Int(nullable: false, identity: true),
                        ExpenseDate = c.DateTime(nullable: false, storeType: "date"),
                        ProductId = c.Int(),
                        Quantity = c.Int(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 40, unicode: false),
                        CreatedAt = c.DateTime(),
                        TimeStamp = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 40, unicode: false),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.ExpenseId)
                .ForeignKey("dbo.SupProducts", t => t.ProductId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        SupId = c.Int(nullable: false, identity: true),
                        SupTel = c.String(maxLength: 10, fixedLength: true),
                        SupName = c.String(nullable: false, maxLength: 40, unicode: false),
                        Email = c.String(nullable: false, maxLength: 40, unicode: false),
                        CreatedBy = c.String(nullable: false, maxLength: 40, unicode: false),
                        CreatedAt = c.DateTime(),
                        TimeStamp = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 40, unicode: false),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.SupId);
            
            CreateTable(
                "dbo.MonthlySummaries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EndDate = c.DateTime(nullable: false),
                        ClosingBalance = c.Decimal(nullable: false, storeType: "money"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sysdiagrams",
                c => new
                    {
                        diagram_id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 128),
                        principal_id = c.Int(nullable: false),
                        version = c.Int(),
                        definition = c.Binary(),
                    })
                .PrimaryKey(t => t.diagram_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Celebrants", "UserName", "dbo.Members");
            DropForeignKey("dbo.SupProducts", "SupId", "dbo.Suppliers");
            DropForeignKey("dbo.expenses", "ProductId", "dbo.SupProducts");
            DropForeignKey("dbo.SupProducts", "EventId", "dbo.Celebrations");
            DropForeignKey("dbo.Celebrations", "EventTypeId", "dbo.EventType");
            DropForeignKey("dbo.Celebrants", "EventId", "dbo.Celebrations");
            DropForeignKey("dbo.Subscriptions", "UserName", "dbo.Members");
            DropForeignKey("dbo.Members", "MemberStatus", "dbo.MemberStatus");
            DropForeignKey("dbo.Deposits", "UserName", "dbo.Members");
            DropForeignKey("dbo.ChatRooms", "UserName", "dbo.Members");
            DropForeignKey("dbo.ActivityLogs", "UserName", "dbo.Members");
            DropIndex("dbo.expenses", new[] { "ProductId" });
            DropIndex("dbo.SupProducts", new[] { "SupId" });
            DropIndex("dbo.SupProducts", new[] { "EventId" });
            DropIndex("dbo.Celebrations", new[] { "EventTypeId" });
            DropIndex("dbo.Celebrants", new[] { "EventId" });
            DropIndex("dbo.Celebrants", new[] { "UserName" });
            DropIndex("dbo.Subscriptions", new[] { "UserName" });
            DropIndex("dbo.Deposits", new[] { "UserName" });
            DropIndex("dbo.ChatRooms", new[] { "UserName" });
            DropIndex("dbo.Members", new[] { "MemberStatus" });
            DropIndex("dbo.ActivityLogs", new[] { "UserName" });
            DropTable("dbo.Sysdiagrams");
            DropTable("dbo.MonthlySummaries");
            DropTable("dbo.Suppliers");
            DropTable("dbo.expenses");
            DropTable("dbo.SupProducts");
            DropTable("dbo.EventType");
            DropTable("dbo.Celebrations");
            DropTable("dbo.Celebrants");
            DropTable("dbo.Subscriptions");
            DropTable("dbo.MemberStatus");
            DropTable("dbo.Deposits");
            DropTable("dbo.ChatRooms");
            DropTable("dbo.Members");
            DropTable("dbo.ActivityLogs");
        }
    }
}
