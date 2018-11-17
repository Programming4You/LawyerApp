using System.Data.Entity;
using Advokati.Infrastructure.Model;
using Task = Advokati.Infrastructure.Model.Task;


namespace Advokati.Infrastructure
{
    public class AdvokatiContext : DbContext
    {
        public AdvokatiContext() : base("name=AdvokatiConnectionString")
        {

        }


        public DbSet<Korisnik> Korisnici { get; set; }
        public DbSet<Advokat> Advokati { get; set; }
        public DbSet<Klijent> Klijenti { get; set; }
        public DbSet<Predmet> Predmeti { get; set; }
        public DbSet<Podpredmet> Podpredmeti { get; set; }
        public DbSet<Task> Taskovi { get; set; }
        public DbSet<Uloga> Uloge { get; set; }

    }
}
