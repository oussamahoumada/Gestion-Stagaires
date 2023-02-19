using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Gestion_Stagaires__Al_Omrane_.Models;

namespace Gestion_Stagaires__Al_Omrane_.Controllers
{
    public class FinStageController : Controller
    {
        private Gestion_StagairesEntities db = new Gestion_StagairesEntities();

        public ActionResult Index()
        {
            if (Session["adm"] == null)
                return Redirect("/Login/Index");

            var stagaires = db.Stagaires;
            List<Stagaires> stg = new List<Stagaires>();
            foreach(var it in stagaires)
            {
                if (it.Date_Debut!=null && it.Durée!=null)
                {
                    int d = int.Parse(it.Durée[0].ToString());
                    TimeSpan dif = ((TimeSpan)(it.Date_Debut- DateTime.Now));
                
                    if ((dif.Days >= 30 && d==1) || (dif.Days >= 60 && d == 2) || (dif.Days >= 90 && d == 3) || (dif.Days >= 120 && d == 4) || (dif.Days >= 150 && d == 5) || (dif.Days >= 180 && d == 6))
                    {
                        stg.Add(it);
                    }
                }
            }
            return View(stg.OrderBy(it=>it.Date_Debut).ToList());
        }

        public ActionResult Chercher(string cin)
        {
            var liststg = db.Stagaires.Where(it => it.CIN == cin);

            return View("Index", liststg.ToList());
        }

        public ActionResult Modifier(string id)
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
        public ActionResult Modifier([Bind(Include = "CIN,Rapport,Depos_Rappoert,Note")] Stagaires stagaires)
        {
            string chemindoc = "";

            HttpPostedFileBase file = Request.Files[0];

            if (file != null && file.ContentLength > 0)
            {
                chemindoc = Path.GetFileName(file.FileName);

                string dirdocs = Server.MapPath("~/Docs/");

                file.SaveAs(dirdocs + chemindoc);
            }

            Stagaires stg = db.Stagaires.Find(stagaires.CIN);
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrWhiteSpace(chemindoc))
                {
                    stg.Depos_Rappoert = true;
                }
                stg.Rapport = chemindoc;
                stg.Note = stagaires.Note;
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
