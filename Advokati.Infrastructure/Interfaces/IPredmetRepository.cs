using System.Collections.Generic;
using Advokati.Infrastructure.Model;

namespace Advokati.Infrastructure.Interfaces
{
    public interface IPredmetRepository
    {
        void Add(Predmet p);
        void Edit(Predmet p);
        void Remove(int id);
        IEnumerable<Predmet> GetPredmeti();
        Predmet FindById(int id);
    }
}
