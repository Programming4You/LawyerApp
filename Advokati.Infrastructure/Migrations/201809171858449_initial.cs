namespace Advokati.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Advokats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ime = c.String(nullable: false, maxLength: 50),
                        Prezime = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Klijents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Naziv = c.String(nullable: false, maxLength: 50),
                        AdvokatId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Advokats", t => t.AdvokatId, cascadeDelete: true)
                .Index(t => t.AdvokatId);
            
            CreateTable(
                "dbo.Korisniks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Podpredmets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NazivPodpredmeta = c.String(nullable: false, maxLength: 50),
                        Sifra = c.String(nullable: false, maxLength: 50),
                        PredmetId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Predmets", t => t.PredmetId, cascadeDelete: true)
                .Index(t => t.PredmetId);
            
            CreateTable(
                "dbo.Predmets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NazivPredmeta = c.String(nullable: false, maxLength: 50),
                        Sifra = c.String(nullable: false, maxLength: 50),
                        KlijentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Klijents", t => t.KlijentId, cascadeDelete: true)
                .Index(t => t.KlijentId);
            
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Datum = c.DateTime(nullable: false),
                        UtrosenoVreme = c.Int(nullable: false),
                        Opis = c.String(nullable: false, maxLength: 150),
                        AdvokatId = c.Int(nullable: false),
                        PodpredmetId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Advokats", t => t.AdvokatId, cascadeDelete: true)
                .ForeignKey("dbo.Podpredmets", t => t.PodpredmetId, cascadeDelete: false)
                .Index(t => t.AdvokatId)
                .Index(t => t.PodpredmetId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tasks", "PodpredmetId", "dbo.Podpredmets");
            DropForeignKey("dbo.Tasks", "AdvokatId", "dbo.Advokats");
            DropForeignKey("dbo.Podpredmets", "PredmetId", "dbo.Predmets");
            DropForeignKey("dbo.Predmets", "KlijentId", "dbo.Klijents");
            DropForeignKey("dbo.Klijents", "AdvokatId", "dbo.Advokats");
            DropIndex("dbo.Tasks", new[] { "PodpredmetId" });
            DropIndex("dbo.Tasks", new[] { "AdvokatId" });
            DropIndex("dbo.Predmets", new[] { "KlijentId" });
            DropIndex("dbo.Podpredmets", new[] { "PredmetId" });
            DropIndex("dbo.Klijents", new[] { "AdvokatId" });
            DropTable("dbo.Tasks");
            DropTable("dbo.Predmets");
            DropTable("dbo.Podpredmets");
            DropTable("dbo.Korisniks");
            DropTable("dbo.Klijents");
            DropTable("dbo.Advokats");
        }
    }
}
