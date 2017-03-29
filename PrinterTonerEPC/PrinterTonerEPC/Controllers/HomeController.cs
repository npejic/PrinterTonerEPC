using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PrinterTonerEPC.DAL;
using PrinterToner.Models;
using System.Data;
using System.Data.Entity;
using System.Net;

namespace PrinterTonerEPC.Controllers
{
    public class HomeController : Controller
    {
        private PrinterTonerContext db = new PrinterTonerContext();

        public ActionResult Index()
        {
            //Izračunava ukupan broj EPC štampača na iznajmljivanju
            PrinterTonerContext db = new PrinterTonerContext();
            var sales = from s in db.Sales
                        where s.Printer.Owner.OwnerName == "EPC DOO"
                        select s;
            var CountRentedPrinters = sales.Count();
            ViewData["CountRentedPrinters"] = CountRentedPrinters;

            //list of opened todoes (without closing date), shown on top of the HomeIndexView
            var openedTasks = db.ToDoes.Include(t => t.User).Where(c => c.Closed == null).OrderBy(c => c.Created).ToList();
            return View(openedTasks);
        }

        public ActionResult Help()
        {
            
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Login(string userName, string password)
        {
            if ("admin".Equals(userName) && "konzola555".Equals(password))
            {
                return RedirectToAction("Index", "Home");
            }
        
            if (db.Users.Any(a => a.Username == userName && a.Password == password))
            {
                var currentClient = db.Users.Where(a => a.Username == userName && a.Password == password).FirstOrDefault();
                if(currentClient.IsAdmin)
                { Session["userIsAdministrator"] = "Administrator"; }
                else
                { Session["userIsAdministrator"] = "Client"; }
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}