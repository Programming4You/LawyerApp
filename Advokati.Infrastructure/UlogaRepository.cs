using System;
using System.Collections.Generic;
using System.Linq;
using Advokati.Infrastructure.Interfaces;
using Advokati.Infrastructure.Model;


namespace Advokati.Infrastructure
{
    public class UlogaRepository : IUlogaRepository
    {
        private readonly AdvokatiContext _context = new AdvokatiContext();

        public void Add(Uloga u)
        {
            throw new NotImplementedException();
        }

        public void Edit(Uloga u)
        {
            throw new NotImplementedException();
        }

        public Uloga FindById(int id)
        {
            var result = (from u in _context.Uloge where u.Id == id select u).FirstOrDefault();
            return result;
        }

        public IEnumerable<Uloga> GetUloge()
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
