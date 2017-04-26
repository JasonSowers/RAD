namespace RAD_Web_API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CLStatus",
                c => new
                    {
                        CLStatuID = c.Int(nullable: false, identity: true),
                        Status = c.String(),
                        LastUpdate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CLStatuID);
            
            CreateTable(
                "dbo.Histories",
                c => new
                    {
                        HistoryID = c.Int(nullable: false, identity: true),
                        RegionName = c.String(),
                        UserID = c.String(),
                        Action = c.String(),
                        Reason = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.HistoryID);
            
            CreateTable(
                "dbo.IACErrors",
                c => new
                    {
                        IACErrorID = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.IACErrorID);
            
            CreateTable(
                "dbo.Regions",
                c => new
                    {
                        RegionID = c.Int(nullable: false, identity: true),
                        RegionName = c.String(),
                        Status = c.String(),
                        RegionDate = c.DateTime(nullable: false),
                        TimeUntilDown = c.Int(nullable: false),
                        EstimatedTimeDown = c.Int(nullable: false),
                        Comment = c.String(),
                    })
                .PrimaryKey(t => t.RegionID);
            
            CreateTable(
                "dbo.Settings",
                c => new
                    {
                        SettingID = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        HideRegions = c.String(),
                        TSOID = c.String(),
                        UserSettings = c.String(),
                        EmailID = c.String(),
                    })
                .PrimaryKey(t => t.SettingID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Settings");
            DropTable("dbo.Regions");
            DropTable("dbo.IACErrors");
            DropTable("dbo.Histories");
            DropTable("dbo.CLStatus");
        }
    }
}
