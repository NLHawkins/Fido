namespace PickMyBeer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class savematch : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SavedMatches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MatchId = c.Int(nullable: false),
                        PCId = c.Int(nullable: false),
                        BCId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BarClients", t => t.BCId, cascadeDelete: false)
                .ForeignKey("dbo.Matches", t => t.MatchId, cascadeDelete: false)
                .ForeignKey("dbo.PatronClients", t => t.PCId, cascadeDelete: false)
                .Index(t => t.MatchId)
                .Index(t => t.PCId)
                .Index(t => t.BCId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SavedMatches", "PCId", "dbo.PatronClients");
            DropForeignKey("dbo.SavedMatches", "MatchId", "dbo.Matches");
            DropForeignKey("dbo.SavedMatches", "BCId", "dbo.BarClients");
            DropIndex("dbo.SavedMatches", new[] { "BCId" });
            DropIndex("dbo.SavedMatches", new[] { "PCId" });
            DropIndex("dbo.SavedMatches", new[] { "MatchId" });
            DropTable("dbo.SavedMatches");
        }
    }
}
