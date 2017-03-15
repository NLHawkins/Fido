namespace PickMyBeer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class excludeAppUser : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.AppClients");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.AppClients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
