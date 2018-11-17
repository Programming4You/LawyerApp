namespace Advokati.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class uloga : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Korisniks");
            CreateTable(
                "dbo.Ulogas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Naziv = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.Korisniks", "Id", c => c.Int(nullable: false, identity:false));
            AddPrimaryKey("dbo.Korisniks", "Id");
            CreateIndex("dbo.Korisniks", "Id");
            AddForeignKey("dbo.Korisniks", "Id", "dbo.Advokats", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Korisniks", "Id", "dbo.Advokats");
            DropIndex("dbo.Korisniks", new[] { "Id" });
            DropPrimaryKey("dbo.Korisniks");
            AlterColumn("dbo.Korisniks", "Id", c => c.Int(nullable: false, identity: true));
            DropTable("dbo.Ulogas");
            AddPrimaryKey("dbo.Korisniks", "Id");
        }
    }
}
