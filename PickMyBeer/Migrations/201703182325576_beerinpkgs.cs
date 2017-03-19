namespace PickMyBeer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class beerinpkgs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BeerInPkgs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        BarClientId = c.Int(nullable: false),
                        BeerId = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BarClients", t => t.BarClientId, cascadeDelete: true)
                .ForeignKey("dbo.Beers", t => t.BeerId, cascadeDelete: true)
                .Index(t => t.BarClientId)
                .Index(t => t.BeerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BeerInPkgs", "BeerId", "dbo.Beers");
            DropForeignKey("dbo.BeerInPkgs", "BarClientId", "dbo.BarClients");
            DropIndex("dbo.BeerInPkgs", new[] { "BeerId" });
            DropIndex("dbo.BeerInPkgs", new[] { "BarClientId" });
            DropTable("dbo.BeerInPkgs");
        }
    }
}
