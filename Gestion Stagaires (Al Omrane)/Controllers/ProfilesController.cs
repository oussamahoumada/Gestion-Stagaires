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
    public class ProfilesController : Controller
    {
        private Gestion_StagairesEntities db = new Gestion_StagairesEntities();

        public ActionResult Index()
        {
            if (Session["adm"] == null)
                return Redirect("/Login/Index");

            var profiles = db.Profiles.Include(p => p.Spécialité);
            return View(profiles.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profiles profiles = db.Profiles.Find(id);
            if (profiles == null)
            {
                return HttpNotFound();
            }
            return View(profiles);
        }

        public ActionResult Create()
        {
            if (Session["adm"] == null)
                return Redirect("/Login/Index");

            ViewBag.Spés = new SelectList(db.Spécialité, "Id_S", "Nom");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_P,CV,Libelle,Experience,Compétence,ProjetRealisé,Spés")] Profiles profiles)
        {
            string chemindoc = "";
            string chemindoc1 = "";
            string chemindoc2 = "";
            string chemindoc3 = "";
            HttpPostedFileBase file = Request.Files[0];
            HttpPostedFileBase file1 = Request.Files[1];
            HttpPostedFileBase file2 = Request.Files[2];
            HttpPostedFileBase file3 = Request.Files[3];

            if (file != null && file.ContentLength > 0)
            {
                chemindoc = Path.GetFileName(file.FileName);

                string dirdocs = Server.MapPath("~/Docs/");

                file.SaveAs(dirdocs + chemindoc);
            }
            if (file1 != null && file1.ContentLength > 0)
            {
                chemindoc1 = Path.GetFileName(file1.FileName);

                string dirdocs1 = Server.MapPath("~/Docs/");

                file1.SaveAs(dirdocs1 + chemindoc1);
            }
            if (file2 != null && file2.ContentLength > 0)
            {
                chemindoc2 = Path.GetFileName(file2.FileName);

                string dirdocs2 = Server.MapPath("~/Docs/");

                file2.SaveAs(dirdocs2 + chemindoc2);
            }
            if (file3 != null && file3.ContentLength > 0)
            {
                chemindoc3 = Path.GetFileName(file3.FileName);

                string dirdocs3 = Server.MapPath("~/Docs/");

                file3.SaveAs(dirdocs3 + chemindoc3);
            }
            if (ModelState.IsValid)
            {
                profiles.CV = chemindoc;
                profiles.Experience = chemindoc1;
                profiles.Compétence = chemindoc2;
                profiles.ProjetRealisé = chemindoc3;
                db.Profiles.Add(profiles);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Spés = new SelectList(db.Spécialité, "Id_S", "Nom", profiles.Spés);
            return View(profiles);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profiles profiles = db.Profiles.Find(id);
            if (profiles == null)
            {
                return HttpNotFound();
            }
            ViewBag.Spés = new SelectList(db.Spécialité, "Id_S", "Nom", profiles.Spés);
            return View(profiles);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_P,Libelle,CV,Experience,Compétence,ProjetRealisé,Spés")] Profiles profiles)
        {
            Profiles p = db.Profiles.Find(profiles.Id_P);

            string chemindoc = "";
            string chemindoc1 = "";
            string chemindoc2 = "";
            string chemindoc3 = "";
            HttpPostedFileBase file = Request.Files[0];
            HttpPostedFileBase file1 = Request.Files[1];
            HttpPostedFileBase file2 = Request.Files[2];
            HttpPostedFileBase file3 = Request.Files[3];

            if (file != null && file.ContentLength > 0)
            {
                chemindoc = Path.GetFileName(file.FileName);

                string dirdocs = Server.MapPath("~/Docs/");

                file.SaveAs(dirdocs + chemindoc);
            }
            if (file1 != null && file1.ContentLength > 0)
            {
                chemindoc1 = Path.GetFileName(file1.FileName);

                string dirdocs1 = Server.MapPath("~/Docs/");

                file1.SaveAs(dirdocs1 + chemindoc1);
            }
            if (file2 != null && file2.ContentLength > 0)
            {
                chemindoc2 = Path.GetFileName(file2.FileName);

                string dirdocs2 = Server.MapPath("~/Docs/");

                file2.SaveAs(dirdocs2 + chemindoc2);
            }
            if (file3 != null && file3.ContentLength > 0)
            {
                chemindoc3 = Path.GetFileName(file3.FileName);

                string dirdocs3 = Server.MapPath("~/Docs/");

                file3.SaveAs(dirdocs3 + chemindoc3);
            }

            if (ModelState.IsValid)
            {
                if (!string.IsNullOrWhiteSpace(chemindoc)) p.CV = chemindoc;
                else p.CV = profiles.CV;

                if (!string.IsNullOrWhiteSpace(chemindoc1))  p.Experience = chemindoc1;
                else p.Experience = profiles.Experience;

                if (!string.IsNullOrWhiteSpace(chemindoc2)) p.Compétence = chemindoc2;
                else p.Compétence = profiles.Compétence;

                if (!string.IsNullOrWhiteSpace(chemindoc3)) p.ProjetRealisé = chemindoc3;
                else p.ProjetRealisé = profiles.ProjetRealisé;

                p.Libelle = profiles.Libelle;
                p.Spés = profiles.Spés;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Spés = new SelectList(db.Spécialité, "Id_S", "Nom", profiles.Spés);
            return View(profiles);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profiles profiles = db.Profiles.Find(id);
            if (profiles == null)
            {
                return HttpNotFound();
            }
            return View(profiles);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Profiles profiles = db.Profiles.Find(id);
            db.Profiles.Remove(profiles);
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
