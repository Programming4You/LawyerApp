using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Advokati.Infrastructure;
using Advokati.Infrastructure.Model;

namespace Advokati.Web.Controllers
{
    public class PodpredmetsController : Controller
    {
        private readonly PodpredmetRepository _db = new PodpredmetRepository();
        private readonly PredmetRepository _pDb = new PredmetRepository();
        

        // GET: Podpredmets
        public ActionResult Index(string searchString)
        {
            var podpredmeti = from p in _db.GetPodpredmeti()
                              select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                podpredmeti = podpredmeti.Where(p => p.Predmet.NazivPredmeta.Contains(searchString)).ToList();
            }

            return View(podpredmeti);
        }

  

        // GET: Podpredmets/Create
        public ActionResult Create()
        {
            ViewBag.PredmetId = new SelectList(_pDb.GetPredmeti(), "Id", "NazivPredmeta");
            return View();
        }

        // POST: Podpredmets/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NazivPodpredmeta,Sifra,PredmetId")] Podpredmet podpredmet)
        {
            if (ModelState.IsValid)
            {
                _db.Add(podpredmet);

                TempData["Success"] = "Uspešno ste dodali podpredmet";
                return RedirectToAction("Index");
            }

            return View(podpredmet);
        }




        // GET: Podpredmets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Podpredmet podpredmet = _db.FindById(Convert.ToInt32(id));
            if (podpredmet == null)
            {
                return HttpNotFound();
            }

            ViewBag.PredmetId = new SelectList(_pDb.GetPredmeti(), "Id", "NazivPredmeta");

            return View(podpredmet);
        }

        // POST: Podpredmets/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NazivPodpredmeta,Sifra,PredmetId")] Podpredmet podpredmet)
        {

            if (ModelState.IsValid)
            {
                _db.Edit(podpredmet);

                TempData["Success"] = "Uspešno izmenjen podpredmet!";
                return RedirectToAction("Index");
            }

            return View(podpredmet);
        }

        // GET: Podpredmets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Podpredmet podpredmet = _db.FindById(Convert.ToInt32(id));
            if (podpredmet == null)
            {
                return HttpNotFound();
            }
            return View(podpredmet);
        }

        // POST: Podpredmets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _db.Remove(id);

            TempData["Success"] = "Uspešno obrisan podpredmet!";
            return RedirectToAction("Index");
        }



    }
}
