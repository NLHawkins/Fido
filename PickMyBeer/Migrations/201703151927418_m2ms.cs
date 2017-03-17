namespace PickMyBeer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m2ms : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BeerArchives",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        BarClientId = c.Int(nullable: false),
                        BeerId = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BeerOnTaps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        BarClientId = c.Int(nullable: false),
                        BeerId = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FaveBeers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        PatronClientId = c.Int(nullable: false),
                        BeerId = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PrevSearchedBeers");
            DropTable("dbo.FaveBeers");
            DropTable("dbo.BeerOnTaps");
            DropTable("dbo.BeerArchives");
        }
    }
}
