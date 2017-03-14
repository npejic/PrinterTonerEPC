﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PrinterTonerEPC.DAL;
using PrinterToner.Models;


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
            //User ClientToCheck = new User {Username=userName, Password = password};
            if (db.Users.Any(a => a.Username == userName && a.Password == password))
            {
                var currentClient = db.Users.Where(a => a.Username == userName && a.Password == password).FirstOrDefault();
                if(currentClient.IsAdmin)
                { Session["userIsAdministrator"] = "Administrator"; }
                else
                { Session["userIsAdministrator"] = "Client"; }
                return RedirectToAction("Index", "Home");
            }
            
            //if ("simic".Equals(userName) && "ognjen".Equals(password)) 
            //{
            //    Session["user"] = "Ognjen";
            //    return RedirectToAction("Index", "Home");
            //}

            return View();
        }
    }
}