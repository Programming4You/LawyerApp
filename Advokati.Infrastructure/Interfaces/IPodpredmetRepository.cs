using System.Collections.Generic;
using Advokati.Infrastructure.Model;

namespace Advokati.Infrastructure.Interfaces
{
    public interface IPodpredmetRepository
    {
        void Add(Podpredmet p);
        void Edit(Podpredmet p);
        void Remove(int id);
        IEnumerable<Podpredmet> GetPodpredmeti();
        Podpredmet FindById(int id);
    }
}
