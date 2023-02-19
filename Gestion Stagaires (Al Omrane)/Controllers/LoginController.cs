using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Gestion_Stagaires__Al_Omrane_.Models;

namespace Gestion_Stagaires__Al_Omrane_.Controllers
{
    public class LoginController : Controller
    {
        Gestion_StagairesEntities db = new Gestion_StagairesEntities();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authentifier(Administrateur Admin) 
        {
            Administrateur adm = db.Administrateur.Where(item => item.Email == Admin.Email && item.MotdePass == Admin.MotdePass).FirstOrDefault();
            
            if (adm == null)
            {
                //return Content("Exist pas"); return une page vide avec un message  (Exist pas)
                return View("Index");
            }
            else
            {
                Session["adm"] = Admin;
                return Redirect("/Home/Index");
            }
        }
        public ActionResult LogOut()
        {
            Session["adm"] = null;
            return Redirect("/Login/Index");
        }
    }
}