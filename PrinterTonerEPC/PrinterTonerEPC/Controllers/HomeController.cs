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
            //Izračunava ukupan broj EPC štampača na iznajmljivanju
            PrinterTonerContext db = new PrinterTonerContext();
            var sales = from s in db.Sales
                        where s.Printer.Owner.OwnerName == "EPC DOO"
                        select s;
            var CountRentedPrinters = sales.Count();
            ViewData["CountRentedPrinters"] = CountRentedPrinters;
            return View();
        }

        public ActionResult Help()
        {
            
            return View();
        }


        //GET
        public ActionResult Login()
        {
            

            return View();
        }
        
        [HttpPost]
        public ActionResult Login(string userName, string password)
        {
            if ("simic".Equals(userName) && "ognjen".Equals(password)) 
            {
                Session["user"] = "Ognjen";
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
    }
}