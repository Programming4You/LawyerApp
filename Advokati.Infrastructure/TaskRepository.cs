using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Advokati.Infrastructure.Interfaces;
using Advokati.Infrastructure.Model.ViewModel;
using Task = Advokati.Infrastructure.Model.Task;


namespace Advokati.Infrastructure
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AdvokatiContext _context = new AdvokatiContext();

        public void Add(Task t)
        {
            _context.Taskovi.Add(t);
            _context.SaveChanges();
        }

        public void Edit(Task t)
        {
            _context.Entry(t).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }

        public IEnumerable<AdvokatKlijentVM> FindByAdvokat(int advokatId)
        {
            var result = (from t in _context.Taskovi
                join pp in _context.Podpredmeti on t.PodpredmetId equals pp.Id
                join p in _context.Predmeti on pp.PredmetId equals p.Id
                join k in _context.Klijenti on p.KlijentId equals k.Id
                where t.AdvokatId == advokatId
                select new AdvokatKlijentVM
                {
                    Id = t.Id,
                    Naziv = k.Naziv,
                    Podpredmet = pp.NazivPodpredmeta,
                    Datum = t.Datum,
                    UtrosenoVreme = t.UtrosenoVreme,
                    Opis = t.Opis,
                    Placeno = t.Placeno

                }).ToList();

            return result;
        }

 

        public Task FindById(int id)
        {
            var result =
                (from t in _context.Taskovi.Include(p => p.Podpredmet).Include(a => a.Advokat)
                    where t.Id == id
                    select t).FirstOrDefault();
            return result;
        }


        public IEnumerable<Task> FindByKlijent(int klijentId)
        {
            var result = (from t in _context.Taskovi
                          join pp in _context.Podpredmeti on t.PodpredmetId equals pp.Id
                          join p in _context.Predmeti on pp.PredmetId equals p.Id
                          join k in _context.Klijenti on p.KlijentId equals k.Id
                          where k.Id == klijentId
                          select t).Include(pp => pp.Podpredmet).Include(a => a.Advokat).ToList();

            return result;
        }

        public IEnumerable<StatistikaVM> GetStatistika()
        {

            var result = (from t in _context.Taskovi
                          join pp in _context.Podpredmeti on t.PodpredmetId equals pp.Id
                          join a in _context.Advokati on t.AdvokatId equals a.Id
                          join k in _context.Klijenti on a.Id equals k.AdvokatId
                          group new { t, pp, a, k } by new { a.Prezime } into g
                         //group a by a.Ime + " " + a.Prezime into g
                          select new StatistikaVM
                          {
                              Ime = g.FirstOrDefault().a.Ime + " " + g.FirstOrDefault().a.Prezime,
                              BrojTaskova = g.Select(y => y.t.Id).Distinct().Count(),
                              BrojKlijenata = g.Select(y => y.k.Id).Distinct().Count(),
                              BrojPodpredmeta = g.Select(y => y.pp.Id).Distinct().Count(),
                              UkupnoVreme = g.Select(x => x.t.UtrosenoVreme).Distinct().Sum(),
                              Zarada = g.Select(y => y.t.UtrosenoVreme*50).Distinct().Sum()
                             // Zarada = g.Distinct().Sum(x => x.t.UtrosenoVreme * 50)
                          }).ToList();

            return result;

        }

        public IEnumerable<Task> GetTasks()
        {
            return _context.Taskovi.Include(p => p.Podpredmet).Include(a => a.Advokat);
        }


        public void Remove(int id)
        {
            Task t = _context.Taskovi.Find(id);
            _context.Taskovi.Remove(t);
            _context.SaveChanges();
        }

      
    }
}
