using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Advokati.Infrastructure;
using Advokati.Infrastructure.Model;

namespace Advokati.Web.Controllers
{
    public class PredmetsController : Controller
    {
        private readonly PredmetRepository _db = new PredmetRepository();
        private readonly KlijentRepository _kljDb = new KlijentRepository();

        // GET: Predmets
        public ActionResult Index(string searchString)
        {
            var predmeti = from p in _db.GetPredmeti()
                           select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                predmeti = predmeti.Where(p => p.NazivPredmeta.Contains(searchString) || p.Klijent.Naziv.Contains(searchString)).ToList();
            }

            return View(predmeti);
        }

 

        // GET: Predmets/Create
        public ActionResult Create()
        {
            ViewBag.KlijentId = new SelectList(_kljDb.GetKlijenti(), "Id", "Naziv");
            return View();
        }

        // POST: Predmets/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NazivPredmeta,Sifra,KlijentId")] Predmet predmet)
        {
            if (ModelState.IsValid)
            {
                _db.Add(predmet);

                TempData["Success"] = "Uspešno dodat predmet";
                return RedirectToAction("Index");
            }

            return View(predmet);
        }



        // GET: Predmets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Predmet predmet = _db.FindById(Convert.ToInt32(id));
            if (predmet == null)
            {
                return HttpNotFound();
            }

            ViewBag.KlijentId = new SelectList(_kljDb.GetKlijenti(), "Id", "Naziv");

            return View(predmet);
        }

        // POST: Predmets/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NazivPredmeta,Sifra,KlijentId")] Predmet predmet)
        {

            if (ModelState.IsValid)
            {
                _db.Edit(predmet);

                TempData["Success"] = "Uspešno izmenjen predmet!";
                return RedirectToAction("Index");
            }

            return View(predmet);
        }

        // GET: Predmets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Predmet predmet = _db.FindById(Convert.ToInt32(id));
            if (predmet == null)
            {
                return HttpNotFound();
            }
            return View(predmet);
        }

        // POST: Predmets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _db.Remove(id);

            TempData["Success"] = "Uspešno obrisan predmet!";
            return RedirectToAction("Index");
        }


    }
}
