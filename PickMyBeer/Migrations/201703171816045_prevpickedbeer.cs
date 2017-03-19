namespace PickMyBeer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class prevpickedbeer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PrevPickedBeers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        PrefBeerId = c.Int(nullable: false),
                        MatchBeerId = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        PatronClientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Beers", t => t.MatchBeerId, cascadeDelete: false)
                .ForeignKey("dbo.Beers", t => t.PrefBeerId, cascadeDelete: false)
                .Index(t => t.PrefBeerId)
                .Index(t => t.MatchBeerId);
            
            DropTable("dbo.PrevSearchedBeers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PrevSearchedBeers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        BeerId = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        PatronClientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.PrevPickedBeers", "PrefBeerId", "dbo.Beers");
            DropForeignKey("dbo.PrevPickedBeers", "MatchBeerId", "dbo.Beers");
            DropIndex("dbo.PrevPickedBeers", new[] { "MatchBeerId" });
            DropIndex("dbo.PrevPickedBeers", new[] { "PrefBeerId" });
            DropTable("dbo.PrevPickedBeers");
        }
    }
}
