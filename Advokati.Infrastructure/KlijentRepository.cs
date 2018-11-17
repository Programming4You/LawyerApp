using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Advokati.Infrastructure.Interfaces;
using Advokati.Infrastructure.Model;


namespace Advokati.Infrastructure
{
    public class KlijentRepository : IKlijentRepository
    {
        private readonly AdvokatiContext _context = new AdvokatiContext();

        public void Add(Klijent k)
        {
            _context.Klijenti.Add(k);
            _context.SaveChanges();
        }

        public void Edit(Klijent k)
        {
            _context.Entry(k).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }

        public Klijent FindById(int id)
        {
            var result = (from k in _context.Klijenti where k.Id == id select k).Include(a => a.Advokat).FirstOrDefault();
            return result;
        }

        public IEnumerable<Klijent> GetKlijenti()
        {
            return _context.Klijenti.Include(a => a.Advokat);
        }

        public void Remove(int id)
        {
            Klijent k = _context.Klijenti.Find(id);
            _context.Klijenti.Remove(k);
            _context.SaveChanges();
        }
    }
}
