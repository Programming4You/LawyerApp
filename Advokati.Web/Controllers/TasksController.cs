using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Advokati.Infrastructure;
using Advokati.Infrastructure.Model.ViewModel;
using Task = Advokati.Infrastructure.Model.Task;
using PagedList;


namespace Advokati.Web.Controllers
{
    public class TasksController : Controller
    {
        private readonly TaskRepository _db = new TaskRepository();
        private readonly AdvokatRepository _advDb = new AdvokatRepository();
        private readonly PodpredmetRepository _ppDb = new PodpredmetRepository();
 


        // GET: Tasks
        public ActionResult Index(string searchAdvokat, string searchPodpredmet, int? price, DateTime? endDate, int? page, string currentFilter)
        {
            if (endDate != null)
            {
                page = 1;
            }
            else
            {
                endDate = string.IsNullOrEmpty(currentFilter) ? (DateTime?)null : DateTime.Parse(currentFilter);
            }

            if (endDate.HasValue)
            {
                ViewBag.CurrentFilter = endDate.Value.ToShortDateString();
            }


            var taskovi = from t in _db.GetTasks().OrderByDescending(d => d.Datum)
                where ((searchAdvokat == null || t.Advokat.Ime.Contains(searchAdvokat) || t.Advokat.Prezime.Contains(searchAdvokat)) &&
                       (searchPodpredmet == null || t.Podpredmet.NazivPodpredmeta.Contains(searchPodpredmet)) &&
                       (price == null || t.Cena <= price) && (endDate == null || t.Datum <= endDate))
                select t;

            int pageSize = 4;
            int pageNumber = (page ?? 1);

            return View(taskovi.ToPagedList(pageNumber, pageSize));
        }

 
        // GET: Tasks/Details/5
        public ActionResult Details(int? id)
        {
            IEnumerable<AdvokatKlijentVM> task = _db.FindByAdvokat(Convert.ToInt32(id));

            return View(task.OrderByDescending(d => d.Datum));
        }



        // GET: Tasks/Detalji/5
        public ActionResult Detalji(int? id)
        {

            IEnumerable<Task> task = _db.FindByKlijent(Convert.ToInt32(id));

            return View(task.OrderByDescending(d => d.Datum));
        }


        public ActionResult Statistika()
        {
            IEnumerable<StatistikaVM> statistika = _db.GetStatistika();

            return View(statistika);
        }


        // GET: Tasks/Create
        public ActionResult Create()
        {
            ViewBag.advokati = _advDb.GetAdvokati().Where(a => a.Ime != "Admin");
            ViewBag.PodpredmetId = new SelectList(_ppDb.GetPodpredmeti(), "Id", "NazivPodpredmeta");
            return View();
        }

        // POST: Tasks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UtrosenoVreme,Opis,AdvokatId,PodpredmetId,Placeno")] Task task)
        {
            if (ModelState.IsValid)
            {
                task.Datum = DateTime.Now;
                _db.Add(task);

                TempData["Success"] = "Uspešno dodat task";
                return RedirectToAction("Index");
            }

            return View(task);
        }



        // GET: Tasks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = _db.FindById(Convert.ToInt32(id));
            if (task == null)
            {
                return HttpNotFound();
            }

            ViewBag.advokati = _advDb.GetAdvokati().Where(a => a.Ime != "Admin");
            ViewBag.PodpredmetId = new SelectList(_ppDb.GetPodpredmeti(), "Id", "NazivPodpredmeta");

            return View(task);
        }

        // POST: Podpredmets/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UtrosenoVreme,Opis,AdvokatId,PodpredmetId,Placeno")] Task task)
        {

            if (ModelState.IsValid)
            {
                task.Datum = DateTime.Now;
                _db.Edit(task);

                TempData["Success"] = "Uspešno izmenjen task!";
                return RedirectToAction("Index");
            }

            return View(task);
        }




        // GET: Tasks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Task task = _db.FindById(Convert.ToInt32(id));
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _db.Remove(id);

            TempData["Success"] = "Uspešno ste obrisali task";
            return RedirectToAction("Index");
        }


    }
}
