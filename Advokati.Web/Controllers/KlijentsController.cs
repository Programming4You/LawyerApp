using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Advokati.Infrastructure;
using Advokati.Infrastructure.Model;
using System.Web.Helpers;


namespace Advokati.Web.Controllers
{
    public class KlijentsController : Controller
    {
        private readonly KlijentRepository _db = new KlijentRepository();
        private readonly AdvokatRepository _advDb = new AdvokatRepository();

        // GET: Klijents
        public ActionResult Index(string searchString)
        {
            var klijenti = from k in _db.GetKlijenti()
                           select k;

            if (!String.IsNullOrEmpty(searchString))
            {
                klijenti = klijenti.Where(k => k.Naziv.Contains(searchString)).ToList();
            }

            return View(klijenti);
        }



        // GET: Klijents/Create
        public ActionResult Create()
        {
            ViewBag.advokati = _advDb.GetAdvokati().Where(a => a.Ime != "Admin");
            return View();
        }

        // POST: Klijents/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Naziv,AdvokatId")] Klijent klijent)
        {
            if (ModelState.IsValid)
            {
                _db.Add(klijent);

                TempData["Success"] = "Uspešno dodat klijent";

                return RedirectToAction("Index");
            }

            return View(klijent);
        }



        // GET: Klijents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Klijent klijent = _db.FindById(Convert.ToInt32(id));
            if (klijent == null)
            {
                return HttpNotFound();
            }
            ViewBag.advokati = _advDb.GetAdvokati().Where(a => a.Ime != "Admin");

            return View(klijent);
        }

        // POST: Klijents/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Naziv,AdvokatId")] Klijent klijent)
        {

            if (ModelState.IsValid)
            {
                _db.Edit(klijent);

                TempData["Success"] = "Uspešno izmenjen klijent!";
                return RedirectToAction("Index");
            }

            return View(klijent);
        }

        // GET: Klijents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Klijent klijent = _db.FindById(Convert.ToInt32(id));
            if (klijent == null)
            {
                return HttpNotFound();
            }
            return View(klijent);
        }

        // POST: Klijents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _db.Remove(id);

            TempData["Success"] = "Uspešno obrisan klijent!";
            return RedirectToAction("Index");
        }




    }
}
