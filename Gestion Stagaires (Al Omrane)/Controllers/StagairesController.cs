using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Gestion_Stagaires__Al_Omrane_.Models;

namespace Gestion_Stagaires__Al_Omrane_.Controllers
{
    public class StagairesController : Controller
    {
        private Gestion_StagairesEntities db = new Gestion_StagairesEntities();

        public ActionResult Index()
        {
            if (Session["adm"] == null)
                return Redirect("/Login/Index");

            var stagaires = db.Stagaires.Include(s => s.Profiles).Include(s => s.Services);
            return View(stagaires.ToList());
        }

        public ActionResult Chercher(string cin)
        {
            var liststg = db.Stagaires.Where(it => it.CIN == cin);

            return View("Index", liststg.ToList());
        }

        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stagaires stagaires = db.Stagaires.Find(id);
            if (stagaires == null)
            {
                return HttpNotFound();
            }
            return View(stagaires);
        }

        public ActionResult Create()
        {
            if (Session["adm"] == null)
                return Redirect("/Login/Index");

            ViewBag.Profil = new SelectList(db.Profiles, "Id_P", "Libelle");
            //ViewBag.S_Service = new SelectList(db.Services, "Numéro", "Nom");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CIN,Nom,Prenom,Email,Tel,Profil")] Stagaires stagaires)
        {
            if (ModelState.IsValid)
            {
                stagaires.Depos_Rappoert = false;
                db.Stagaires.Add(stagaires);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Profil = new SelectList(db.Profiles, "Id_P", "Libelle", stagaires.Profil);
            //ViewBag.S_Service = new SelectList(db.Services, "Numéro", "Nom", stagaires.S_Service);
            return View(stagaires);
        }

        // GET: Stagaires/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stagaires stagaires = db.Stagaires.Find(id);
            if (stagaires == null)
            {
                return HttpNotFound();
            }
            ViewBag.Profil = new SelectList(db.Profiles, "Id_P", "Libelle", stagaires.Profil);
            ViewBag.S_Service = new SelectList(db.Services, "Numéro", "Nom", stagaires.S_Service);
            return View(stagaires);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CIN,Nom,Prenom,Email,Tel,Date_Debut,Sujet,Rapport,Depos_Rappoert,Note,S_Service,Profil,Durée")] Stagaires stagaires)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stagaires).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Profil = new SelectList(db.Profiles, "Id_P", "Libelle", stagaires.Profil);
            ViewBag.S_Service = new SelectList(db.Services, "Numéro", "Nom", stagaires.S_Service);
            return View(stagaires);
        }

        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stagaires stagaires = db.Stagaires.Find(id);
            if (stagaires == null)
            {
                return HttpNotFound();
            }
            return View(stagaires);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Stagaires stagaires = db.Stagaires.Find(id);
            db.Stagaires.Remove(stagaires);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
