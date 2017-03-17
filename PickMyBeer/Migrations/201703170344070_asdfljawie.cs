namespace PickMyBeer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asdfljawie : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Beers", "BarClient_Id", "dbo.BarClients");
            DropForeignKey("dbo.Beers", "BarClient_Id1", "dbo.BarClients");
            DropIndex("dbo.Beers", new[] { "BarClient_Id" });
            DropIndex("dbo.Beers", new[] { "BarClient_Id1" });
            DropColumn("dbo.Beers", "BarClient_Id");
            DropColumn("dbo.Beers", "BarClient_Id1");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Beers", "BarClient_Id1", c => c.Int());
            AddColumn("dbo.Beers", "BarClient_Id", c => c.Int());
            CreateIndex("dbo.Beers", "BarClient_Id1");
            CreateIndex("dbo.Beers", "BarClient_Id");
            AddForeignKey("dbo.Beers", "BarClient_Id1", "dbo.BarClients", "Id");
            AddForeignKey("dbo.Beers", "BarClient_Id", "dbo.BarClients", "Id");
        }
    }
}
