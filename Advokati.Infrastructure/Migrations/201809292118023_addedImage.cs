namespace Advokati.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Advokats", "AdvokatImage", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Advokats", "AdvokatImage");
        }
    }
}
