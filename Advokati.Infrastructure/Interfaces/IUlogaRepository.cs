using System.Collections.Generic;
using Advokati.Infrastructure.Model;

namespace Advokati.Infrastructure.Interfaces
{
    public interface IUlogaRepository
    {
        void Add(Uloga u);
        void Edit(Uloga u);
        void Remove(int id);
        IEnumerable<Uloga> GetUloge();
        Uloga FindById(int id);
    }
}
