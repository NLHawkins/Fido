namespace PickMyBeer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ck5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Ingredients", "Beer_Id", "dbo.Beers");
            DropIndex("dbo.Ingredients", new[] { "Beer_Id" });
            AddColumn("dbo.BarClients", "BarName", c => c.String());
            AddColumn("dbo.PatronClients", "Name", c => c.String());
            AddColumn("dbo.PatronClients", "Age", c => c.Int(nullable: false));
            CreateIndex("dbo.BeerArchives", "BarClientId");
            CreateIndex("dbo.BeerArchives", "BeerId");
            CreateIndex("dbo.IngredientLogs", "IngredientId");
            CreateIndex("dbo.IngredientLogs", "BeerId");
            AddForeignKey("dbo.BeerArchives", "BarClientId", "dbo.BarClients", "Id", cascadeDelete: true);
            AddForeignKey("dbo.IngredientLogs", "BeerId", "dbo.Beers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.IngredientLogs", "IngredientId", "dbo.Ingredients", "Id", cascadeDelete: true);
            AddForeignKey("dbo.BeerArchives", "BeerId", "dbo.Beers", "Id", cascadeDelete: true);
            DropColumn("dbo.Ingredients", "Beer_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ingredients", "Beer_Id", c => c.Int());
            DropForeignKey("dbo.BeerArchives", "BeerId", "dbo.Beers");
            DropForeignKey("dbo.IngredientLogs", "IngredientId", "dbo.Ingredients");
            DropForeignKey("dbo.IngredientLogs", "BeerId", "dbo.Beers");
            DropForeignKey("dbo.BeerArchives", "BarClientId", "dbo.BarClients");
            DropIndex("dbo.IngredientLogs", new[] { "BeerId" });
            DropIndex("dbo.IngredientLogs", new[] { "IngredientId" });
            DropIndex("dbo.BeerArchives", new[] { "BeerId" });
            DropIndex("dbo.BeerArchives", new[] { "BarClientId" });
            DropColumn("dbo.PatronClients", "Age");
            DropColumn("dbo.PatronClients", "Name");
            DropColumn("dbo.BarClients", "BarName");
            CreateIndex("dbo.Ingredients", "Beer_Id");
            AddForeignKey("dbo.Ingredients", "Beer_Id", "dbo.Beers", "Id");
        }
    }
}
