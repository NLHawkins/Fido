namespace PickMyBeer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ck9 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Beers", "PatronClient_Id1", "dbo.PatronClients");
            DropIndex("dbo.Beers", new[] { "PatronClient_Id1" });
            DropColumn("dbo.Beers", "PatronClient_Id1");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Beers", "PatronClient_Id1", c => c.Int());
            CreateIndex("dbo.Beers", "PatronClient_Id1");
            AddForeignKey("dbo.Beers", "PatronClient_Id1", "dbo.PatronClients", "Id");
        }
    }
}
