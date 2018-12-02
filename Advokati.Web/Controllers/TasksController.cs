﻿using System;
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
        public ActionResult Index(string searchAdvokat, string searchPodpredmet, int? price, DateTime? endDate, int? page)
        {
            int pageSize = 4;

            var taskovi = from t in _db.GetTasks().OrderByDescending(d => d.Datum)
                          select t;

            if (!String.IsNullOrEmpty(searchAdvokat) || !String.IsNullOrEmpty(searchPodpredmet) || price.HasValue || endDate.HasValue)
            {
                pageSize = 20;

                if (!String.IsNullOrWhiteSpace(searchAdvokat))
                {
                    taskovi = taskovi.Where(t => t.Advokat.Ime.Contains(searchAdvokat) || t.Advokat.Prezime.Contains(searchAdvokat));
                }

                if (!String.IsNullOrWhiteSpace(searchPodpredmet))
                {
                    taskovi = taskovi.Where(t => t.Podpredmet.NazivPodpredmeta.Contains(searchPodpredmet));
                }

                if (price.HasValue)
                {
                    taskovi = taskovi.Where(t => t.Cena <= price);
                }

                if (endDate.HasValue)
                {
                    taskovi = taskovi.Where(t => t.Datum <= endDate);
                }

            }


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
