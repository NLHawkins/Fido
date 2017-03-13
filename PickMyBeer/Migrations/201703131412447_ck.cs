namespace PickMyBeer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ck : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "UserRole", c => c.String());
            DropColumn("dbo.AspNetUsers", "barClient");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "barClient", c => c.Boolean(nullable: false));
            DropColumn("dbo.AspNetUsers", "UserRole");
        }
    }
}
