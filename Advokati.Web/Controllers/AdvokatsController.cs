using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Advokati.Infrastructure;
using Advokati.Infrastructure.Model;
using PagedList;


namespace Advokati.Web.Controllers
{
    public class AdvokatsController : Controller
    {
        private readonly AdvokatRepository _db = new AdvokatRepository();
        private readonly KorisnikRepository _kDb = new KorisnikRepository();


        // GET: Advokats
        public ActionResult Index(string searchString, int? page)
        {
            var advokati = from a in _db.GetAdvokati().Where(a => a.Ime != "Admin")
                            select new Advokat
                            {
                                Id = a.Id,
                                Ime = a.Ime + " " + a.Prezime,
                                AdvokatImage = a.AdvokatImage
                            };

            if (!String.IsNullOrEmpty(searchString))
            {
                advokati = advokati.Where(a => a.Ime.Contains(searchString)).ToList();
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View(advokati.ToPagedList(pageNumber, pageSize));
        }

 

        // GET: Advokats/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Advokats/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Ime,Prezime,AdvokatImage")] Advokat advokat, HttpPostedFileBase image, Korisnik korisnik)
        {

            if (image != null)
            {
                var allowedContentTypes = new[] { "image/jpeg", "image/jpg", "image/png", "image/gif", "image/tif" };

                if (allowedContentTypes.Contains(image.ContentType))
                {
                    var imagesPath = "/Content/images/";
                    var filename = Guid.NewGuid().ToString() + image.FileName;
                    var uploadPath = imagesPath + filename;
                    var physicalPath = Server.MapPath(uploadPath);
                    image.SaveAs(physicalPath);
                    advokat.AdvokatImage = uploadPath;

                    _db.Add(advokat);

                    korisnik.Username = advokat.Ime;
                    korisnik.Password = advokat.Prezime;
                    korisnik.UlogaId = 2;
                    korisnik.Id = advokat.Id;
                  
                    _kDb.Add(korisnik);

                    TempData["Success"] = "Uspešno dodat advokat";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("Create");
            }


            return View(advokat);
        }



        // GET: Advokats/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Advokat advokat = _db.FindById(Convert.ToInt32(id));
            if (advokat == null)
            {
                return HttpNotFound();
            }
            
            return View(advokat);
        }

        // POST: Advokats/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Ime,Prezime,AdvokatImage")] Advokat advokat, HttpPostedFileBase image)
        {
            if (image != null)
            {
                var allowedContentTypes = new[] { "image/jpeg", "image/jpg", "image/png", "image/gif", "image/tif" };

                if (allowedContentTypes.Contains(image.ContentType))
                {
                    var imagesPath = "/Content/images/";
                    var filename = Guid.NewGuid().ToString() + image.FileName;
                    var uploadPath = imagesPath + filename;
                    var physicalPath = Server.MapPath(uploadPath);
                    image.SaveAs(physicalPath);
                    advokat.AdvokatImage = uploadPath;

                    _db.Edit(advokat);

                    TempData["Success"] = "Uspešno izmenjen advokat";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("Edit");
            }


            return View(advokat);
        }

        // GET: Advokats/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Advokat advokat = _db.FindById(Convert.ToInt32(id));
            if (advokat == null)
            {
                return HttpNotFound();
            }
            return View(advokat);
        }

        // POST: Advokats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _kDb.Remove(id);
            _db.Remove(id);

            TempData["Success"] = "Uspešno obrisan advokat!";
            return RedirectToAction("Index");
        }


    }
}
