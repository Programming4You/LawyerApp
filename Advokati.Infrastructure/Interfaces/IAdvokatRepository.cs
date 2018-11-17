using System.Collections.Generic;
using Advokati.Infrastructure.Model;

namespace Advokati.Infrastructure.Interfaces
{
    public interface IAdvokatRepository
    {
        void Add(Advokat a);
        void Edit(Advokat a);
        void Remove(int id);
        IEnumerable<Advokat> GetAdvokati();
        Advokat FindById(int id);
    }
}
