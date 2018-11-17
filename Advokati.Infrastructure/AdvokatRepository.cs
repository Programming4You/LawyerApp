using System.Collections.Generic;
using System.Linq;
using Advokati.Infrastructure.Interfaces;
using Advokati.Infrastructure.Model;


namespace Advokati.Infrastructure
{
    public class AdvokatRepository : IAdvokatRepository
    {
        private readonly AdvokatiContext _context = new AdvokatiContext();

        public void Add(Advokat a)
        {
            _context.Advokati.Add(a);
            _context.SaveChanges();
        }

        public void Edit(Advokat a)
        {
            _context.Entry(a).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }

        public Advokat FindById(int id)
        {
            var result = (from a in _context.Advokati where a.Id == id select a).FirstOrDefault();
            return result;
        }

        public IEnumerable<Advokat> GetAdvokati()
        {
            return _context.Advokati;
        }


        public void Remove(int id)
        {
            Advokat a = _context.Advokati.Find(id);
            _context.Advokati.Remove(a);
            _context.SaveChanges();
        }
    }
}
