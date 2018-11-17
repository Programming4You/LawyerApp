using System;
using System.Collections.Generic;
using System.Linq;
using Advokati.Infrastructure.Interfaces;
using Advokati.Infrastructure.Model;


namespace Advokati.Infrastructure
{
    public class KorisnikRepository : IKorisnikRepository
    {
        private readonly AdvokatiContext _context = new AdvokatiContext();

        public void Add(Korisnik k)
        {
            _context.Korisnici.Add(k);
            _context.SaveChanges();
        }

        public void Edit(Korisnik k)
        {
            throw new NotImplementedException();
        }

        public Korisnik FindAdmin()
        {
            var result = (from k in _context.Korisnici where k.Username == "admin" && k.Password == "admin" select k).FirstOrDefault();
            return result;
        }

        public Korisnik FindByUsernamePassword(string username, string password)
        {
            var result = (from k in _context.Korisnici where k.Username == username && k.Password == password select k).FirstOrDefault();
            return result;
        }

        public IEnumerable<Korisnik> GetKorisnici()
        {
            return _context.Korisnici;
        }

        public void Remove(int id)
        {
            Korisnik k = _context.Korisnici.Find(id);
            _context.Korisnici.Remove(k);
            _context.SaveChanges();
        }
    }
}
