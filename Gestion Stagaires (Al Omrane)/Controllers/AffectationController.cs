using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Windows;
using System.Windows.Forms;
using Gestion_Stagaires__Al_Omrane_.Models;

namespace Gestion_Stagaires__Al_Omrane_.Controllers
{
    public class AffectationController : Controller
    {
        private Gestion_StagairesEntities db = new Gestion_StagairesEntities();

        public ActionResult Index()
        {
            if (Session["adm"] == null)
                return Redirect("/Login/Index");

            var lists = db.Stagaires.Where(it => it.Date_Debut == null || it.S_Service == 0 || it.S_Service == null);
            return View(lists.ToList());
        }
        public ActionResult Chercher(string cin)
        {
            var liststg = db.Stagaires.Where(it => it.CIN == cin);

            return View("Index", liststg.ToList());
        }
        public ActionResult Affecter(string id)
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
        public ActionResult Affecter([Bind(Include = "CIN,Date_Debut,Durée,Sujet,S_Service")] Stagaires stagaires)
        {
            Stagaires stg = db.Stagaires.Find(stagaires.CIN);
            if (ModelState.IsValid)
            {
                stg.Date_Debut = stagaires.Date_Debut;
                stg.Durée = stagaires.Durée;
                stg.Sujet = stagaires.Sujet;
                stg.S_Service = stagaires.S_Service;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Profil = new SelectList(db.Profiles, "Id_P", "Libelle", stagaires.Profil);
            ViewBag.S_Service = new SelectList(db.Services, "Numéro", "Nom", stagaires.S_Service);
            return View(stagaires);
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
