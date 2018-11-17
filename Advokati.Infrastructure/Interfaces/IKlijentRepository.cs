using System.Collections.Generic;
using Advokati.Infrastructure.Model;

namespace Advokati.Infrastructure.Interfaces
{
    public interface IKlijentRepository
    {
        void Add(Klijent k);
        void Edit(Klijent k);
        void Remove(int id);
        IEnumerable<Klijent> GetKlijenti();
        Klijent FindById(int id);
    }
}
