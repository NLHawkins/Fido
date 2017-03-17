namespace PickMyBeer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Beers", "BarClient_Id", c => c.Int());
            AddColumn("dbo.Beers", "BarClient_Id1", c => c.Int());
            AddColumn("dbo.Beers", "PatronClient_Id", c => c.Int());
            AddColumn("dbo.Beers", "PatronClient_Id1", c => c.Int());
            AddColumn("dbo.Ingredients", "Beer_Id", c => c.Int());
            CreateIndex("dbo.Beers", "BarClient_Id");
            CreateIndex("dbo.Beers", "BarClient_Id1");
            CreateIndex("dbo.Beers", "PatronClient_Id");
            CreateIndex("dbo.Beers", "PatronClient_Id1");
            CreateIndex("dbo.Ingredients", "Beer_Id");
            AddForeignKey("dbo.Ingredients", "Beer_Id", "dbo.Beers", "Id");
            AddForeignKey("dbo.Beers", "BarClient_Id", "dbo.BarClients", "Id");
            AddForeignKey("dbo.Beers", "BarClient_Id1", "dbo.BarClients", "Id");
            AddForeignKey("dbo.Beers", "PatronClient_Id", "dbo.PatronClients", "Id");
            AddForeignKey("dbo.Beers", "PatronClient_Id1", "dbo.PatronClients", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Beers", "PatronClient_Id1", "dbo.PatronClients");
            DropForeignKey("dbo.Beers", "PatronClient_Id", "dbo.PatronClients");
            DropForeignKey("dbo.Beers", "BarClient_Id1", "dbo.BarClients");
            DropForeignKey("dbo.Beers", "BarClient_Id", "dbo.BarClients");
            DropForeignKey("dbo.Ingredients", "Beer_Id", "dbo.Beers");
            DropIndex("dbo.Ingredients", new[] { "Beer_Id" });
            DropIndex("dbo.Beers", new[] { "PatronClient_Id1" });
            DropIndex("dbo.Beers", new[] { "PatronClient_Id" });
            DropIndex("dbo.Beers", new[] { "BarClient_Id1" });
            DropIndex("dbo.Beers", new[] { "BarClient_Id" });
            DropColumn("dbo.Ingredients", "Beer_Id");
            DropColumn("dbo.Beers", "PatronClient_Id1");
            DropColumn("dbo.Beers", "PatronClient_Id");
            DropColumn("dbo.Beers", "BarClient_Id1");
            DropColumn("dbo.Beers", "BarClient_Id");
        }
    }
}
