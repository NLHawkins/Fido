namespace PickMyBeer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class images : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BarImageLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BarImgUpId = c.Int(nullable: false),
                        BarClientId = c.Int(nullable: false),
                        Timestamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ImageUploads", t => t.BarImgUpId, cascadeDelete: true)
                .Index(t => t.BarImgUpId);
            
            CreateTable(
                "dbo.ImageUploads",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        File = c.String(),
                        Timestamp = c.DateTime(nullable: false),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.BarClients", "BImgLog_Id", c => c.Int());
            CreateIndex("dbo.BarClients", "BImgLog_Id");
            AddForeignKey("dbo.BarClients", "BImgLog_Id", "dbo.BarImageLogs", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BarClients", "BImgLog_Id", "dbo.BarImageLogs");
            DropForeignKey("dbo.BarImageLogs", "BarImgUpId", "dbo.ImageUploads");
            DropIndex("dbo.BarImageLogs", new[] { "BarImgUpId" });
            DropIndex("dbo.BarClients", new[] { "BImgLog_Id" });
            DropColumn("dbo.BarClients", "BImgLog_Id");
            DropTable("dbo.ImageUploads");
            DropTable("dbo.BarImageLogs");
        }
    }
}
