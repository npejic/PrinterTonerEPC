using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PrinterTonerEPC.DAL;

namespace PrinterTonerEPC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ReportByOwner(string searchByOwner)
        {
            PrinterTonerContext db = new PrinterTonerContext();
            var owners = from s in db.Owners
                         select s;

            if (!String.IsNullOrEmpty(searchByOwner))
            {
                owners = owners.Where(s => s.OwnerName == searchByOwner);
            }

            return View(owners.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}