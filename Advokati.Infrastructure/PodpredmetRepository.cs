using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Advokati.Infrastructure.Interfaces;
using Advokati.Infrastructure.Model;


namespace Advokati.Infrastructure
{
    public class PodpredmetRepository : IPodpredmetRepository
    {
        private readonly AdvokatiContext _context = new AdvokatiContext();

        public void Add(Podpredmet p)
        {
            _context.Podpredmeti.Add(p);
            _context.SaveChanges();
        }

        public void Edit(Podpredmet p)
        {
            _context.Entry(p).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }

        public Podpredmet FindById(int id)
        {
            var result = (from p in _context.Podpredmeti.Include(pp => pp.Predmet) where p.Id == id select p).FirstOrDefault();
            return result;
        }

        public IEnumerable<Podpredmet> GetPodpredmeti()
        {
            return _context.Podpredmeti.Include(p => p.Predmet);
        }

        public void Remove(int id)
        {
            Podpredmet p = _context.Podpredmeti.Find(id);
            _context.Podpredmeti.Remove(p);
            _context.SaveChanges();
        }
    }
}
