using System.Collections.Generic;
using Advokati.Infrastructure.Model;

namespace Advokati.Infrastructure.Interfaces
{
    public interface IKorisnikRepository
    {
        void Add(Korisnik k);
        void Edit(Korisnik k);
        void Remove(int id);
        IEnumerable<Korisnik> GetKorisnici();
        Korisnik FindByUsernamePassword(string username, string password);
        Korisnik FindAdmin();
    }
}
