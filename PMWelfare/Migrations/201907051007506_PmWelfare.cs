namespace PMWelfare.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PmWelfare : DbMigration
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
                        DOB = c.DateTime(nullable: false, storeType: "date"),
                        MemberStatus = c.Int(),
                        IsAdmin = c.Boolean(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 40, unicode: false),
                        CreatedAt = c.DateTime(),
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
                        chat_id = c.Int(nullable: false, identity: true),
                        user_name = c.String(maxLength: 40, unicode: false),
                        message = c.String(nullable: false, maxLength: 250, unicode: false),
                        posted_at = c.DateTime(),
                        updated_by = c.String(maxLength: 40, unicode: false),
                        updated_at = c.DateTime(),
                        member_UserName = c.String(maxLength: 40, unicode: false),
                    })
                .PrimaryKey(t => t.chat_id)
                .ForeignKey("dbo.Members", t => t.member_UserName)
                .Index(t => t.member_UserName);
            
            CreateTable(
                "dbo.Deposits",
                c => new
                    {
                        dep_id = c.Int(nullable: false, identity: true),
                        user_name = c.String(maxLength: 40, unicode: false),
                        amount = c.Decimal(nullable: false, storeType: "money"),
                        CreatedBy = c.String(nullable: false, maxLength: 40, unicode: false),
                        CreatedAt = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 40, unicode: false),
                        UpdatedAt = c.DateTime(),
                        member_UserName = c.String(maxLength: 40, unicode: false),
                    })
                .PrimaryKey(t => t.dep_id)
                .ForeignKey("dbo.Members", t => t.member_UserName)
                .Index(t => t.member_UserName);
            
            CreateTable(
                "dbo.MemberStatus",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        MemberStatus = c.String(nullable: false, maxLength: 20, unicode: false),
                        CreatedBy = c.String(nullable: false, maxLength: 40, unicode: false),
                        CreatedAt = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 40, unicode: false),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Subscriptions",
                c => new
                    {
                        SubId = c.Int(nullable: false, identity: true),
                        UserName = c.String(maxLength: 40, unicode: false),
                        Amount = c.Decimal(nullable: false, storeType: "money"),
                        SubMonth = c.Byte(nullable: false),
                        SubYear = c.Int(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 40, unicode: false),
                        CreatedAt = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 40, unicode: false),
                        UpdatedAt = c.DateTime(),
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
                        EventId = c.Int(),
                    })
                .PrimaryKey(t => t.CelebId)
                .ForeignKey("dbo.Celebrations", t => t.EventId)
                .ForeignKey("dbo.Members", t => t.UserName)
                .Index(t => t.UserName)
                .Index(t => t.EventId);
            
            CreateTable(
                "dbo.Celebrations",
                c => new
                    {
                        EventId = c.Int(nullable: false, identity: true),
                        EventName = c.String(nullable: false, maxLength: 40, unicode: false),
                        EventDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 40, unicode: false),
                        CreatedAt = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 40, unicode: false),
                        UpdatedAt = c.DateTime(),
                        EventType_Id = c.Int(),
                    })
                .PrimaryKey(t => t.EventId)
                .ForeignKey("dbo.EventType", t => t.EventType_Id)
                .Index(t => t.EventType_Id);
            
            CreateTable(
                "dbo.EventType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(maxLength: 40),
                        CreatedBy = c.String(nullable: false, maxLength: 40),
                        CreatedAt = c.DateTime(),
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
                        UpdatedBy = c.String(maxLength: 40, unicode: false),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.SupId);
            
            CreateTable(
                "dbo.MonthlySummaries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartAt = c.DateTime(),
                        EndAt = c.DateTime(),
                        ClosingBalance = c.Decimal(nullable: false, storeType: "money"),
                        CreatedBy = c.String(nullable: false, maxLength: 40, unicode: false),
                        CreatedAt = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 40, unicode: false),
                        UpdatedAt = c.DateTime(),
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
            DropForeignKey("dbo.Celebrations", "EventType_Id", "dbo.EventType");
            DropForeignKey("dbo.Celebrants", "EventId", "dbo.Celebrations");
            DropForeignKey("dbo.Subscriptions", "UserName", "dbo.Members");
            DropForeignKey("dbo.Members", "MemberStatus", "dbo.MemberStatus");
            DropForeignKey("dbo.Deposits", "member_UserName", "dbo.Members");
            DropForeignKey("dbo.ChatRooms", "member_UserName", "dbo.Members");
            DropForeignKey("dbo.ActivityLogs", "UserName", "dbo.Members");
            DropIndex("dbo.expenses", new[] { "ProductId" });
            DropIndex("dbo.SupProducts", new[] { "SupId" });
            DropIndex("dbo.SupProducts", new[] { "EventId" });
            DropIndex("dbo.Celebrations", new[] { "EventType_Id" });
            DropIndex("dbo.Celebrants", new[] { "EventId" });
            DropIndex("dbo.Celebrants", new[] { "UserName" });
            DropIndex("dbo.Subscriptions", new[] { "UserName" });
            DropIndex("dbo.Deposits", new[] { "member_UserName" });
            DropIndex("dbo.ChatRooms", new[] { "member_UserName" });
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
