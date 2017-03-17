namespace PickMyBeer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asdflasf : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.BeerOnTaps", "BarClientId");
            CreateIndex("dbo.BeerOnTaps", "BeerId");
            AddForeignKey("dbo.BeerOnTaps", "BarClientId", "dbo.BarClients", "Id", cascadeDelete: true);
            AddForeignKey("dbo.BeerOnTaps", "BeerId", "dbo.Beers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BeerOnTaps", "BeerId", "dbo.Beers");
            DropForeignKey("dbo.BeerOnTaps", "BarClientId", "dbo.BarClients");
            DropIndex("dbo.BeerOnTaps", new[] { "BeerId" });
            DropIndex("dbo.BeerOnTaps", new[] { "BarClientId" });
        }
    }
}
