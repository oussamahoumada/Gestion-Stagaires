using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gestion_Stagaires__Al_Omrane_.Models;

namespace Gestion_Stagaires__Al_Omrane_.Controllers
{
    public class HomeController : Controller
    {
        private Gestion_StagairesEntities db = new Gestion_StagairesEntities();
        // GET: Home
        public ActionResult Index(string drop="Tous")
        {
            if (Session["adm"] == null)
                return Redirect("/Login/Index");

            ViewData["nombreStagaires"] = db.Stagaires.Count();
            ViewData["nombreServices"] = db.Services.Count();
            ViewData["nmbrStgAffecter"] = db.Stagaires.Where(it=>it.S_Service>0 && it.Date_Debut!=null).Count();

            ViewData["count"] = countFin();
            //remplire dropdownlist
            List<Services> ls = db.Services.ToList();
            List<SelectListItem> listItem = new List<SelectListItem>();
            listItem.Add(new SelectListItem { Text="Tous" });
            foreach (var o in ls)
            {
                listItem.Add(new SelectListItem{ Text=o.Nom,Value=o.Numéro.ToString()});
            }
            ViewData["drop"] = listItem;
            //
            //.Where(it => it.Date_Debut != null).OrderBy(it => it.Date_Debut)


            //IEnumerable<Services> listserv = db.Services.ToList();
            //Tuple<IEnumerable<Stagaires>, IEnumerable<Services>> tp = new Tuple<IEnumerable<Stagaires>, IEnumerable<Services>>(liststg,listserv);
            IEnumerable<Stagaires> liststg;
            liststg = db.Stagaires.Where(it=>it.Date_Debut!=null && it.S_Service > 0);
            if (drop != "Tous")
            {
                int id = int.Parse(drop);
                liststg = liststg.Where(it => it.S_Service == id);
            }
            return View(liststg);
        }
        public int countFin()
        {
            var stagaires = db.Stagaires;
            List<Stagaires> stg = new List<Stagaires>();
            foreach (var it in stagaires)
            {
                if (it.Date_Debut != null && it.Durée != null)
                {
                    int d = int.Parse(it.Durée[0].ToString());
                    TimeSpan dif = ((TimeSpan)(it.Date_Debut - DateTime.Now));

                    if ((dif.Days >= 30 && d == 1) || (dif.Days >= 60 && d == 2) || (dif.Days >= 90 && d == 3) || (dif.Days >= 120 && d == 4) || (dif.Days >= 150 && d == 5) || (dif.Days >= 180 && d == 6))
                    {
                        stg.Add(it);
                    }
                }
            }
            return stg.Count;
        }
    }
}