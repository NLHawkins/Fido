namespace PickMyBeer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ck1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Bars", newName: "AppClients");
            CreateTable(
                "dbo.BarClients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        UserId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.Patrons");
            DropTable("dbo.Pickers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Pickers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Patrons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.BarClients");
            RenameTable(name: "dbo.AppClients", newName: "Bars");
        }
    }
}
