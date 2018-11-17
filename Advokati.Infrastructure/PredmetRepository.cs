using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Advokati.Infrastructure.Interfaces;
using Advokati.Infrastructure.Model;
using Task = Advokati.Infrastructure.Model.Task;


namespace Advokati.Infrastructure
{
    public class PredmetRepository : IPredmetRepository
    {
        private readonly AdvokatiContext _context = new AdvokatiContext();

        public void Add(Predmet p)
        {
            _context.Predmeti.Add(p);
            _context.SaveChanges();
        }

        public void Edit(Predmet p)
        {
            _context.Entry(p).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }

        public Predmet FindById(int id)
        {
            var result = (from p in _context.Predmeti.Include(k => k.Klijent) where p.Id == id select p).FirstOrDefault();
            return result;
        }

        public IEnumerable<Predmet> GetPredmeti()
        {
            return _context.Predmeti.Include(k => k.Klijent);
        }

        public void Remove(int id)
        {
            Predmet p = _context.Predmeti.Find(id);
            _context.Predmeti.Remove(p);
            _context.SaveChanges();
        }
    }
}
