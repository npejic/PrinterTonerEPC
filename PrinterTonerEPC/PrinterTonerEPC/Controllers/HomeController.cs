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
            //TODO: Izračunati ukupan broj EPC štampača na iznajmljivanju
            PrinterTonerContext db = new PrinterTonerContext();
            var sales = from s in db.Sales
                        select s;
            var CountRentedPrinters = sales.Count();
            ViewData["CountRentedPrinters"] = CountRentedPrinters;
            return View();
        }

        public ActionResult ReportByOwner(string searchByOwner)
        {
            PrinterTonerContext db = new PrinterTonerContext();
            var sales = from s in db.Sales
                         select s;

            if (!string.IsNullOrEmpty(searchByOwner))
            {
                sales = sales.Where(s => s.Contract.Owner.OwnerName.Contains(searchByOwner));// && s.printer.isepcprinter==true);
            }
            
                return View(sales.ToList());
            }

        //TODO: Izbrisati
        public ActionResult ModifyAltContract(string ModifyAlternateContract)//, string ModifyAlternateContract)
        {
            PrinterTonerContext db = new PrinterTonerContext();
            var sales = from s in db.Sales
                        select s;

            if (!String.IsNullOrEmpty(ModifyAlternateContract))
            {
                sales = sales.Where(s => s.Contract.Owner.OwnerName == "nik" && s.Printer.IsEPCprinter == true);
                foreach (var s in sales)
                    s.AlternateContract = ModifyAlternateContract;
                db.SubmitChanges();
            }

            //
            //foreach (var salesModified in db.Sales.Where(x=> x.Contract.Owner.OwnerName == searchByOwner))//.Where(x => x.LocationOfPrinterIs.Equals("U_firmi")).ToList())
            //{
            //    salesModified.AlternateContract = ModifyAlternateContract;
            //}
            //db.SubmitChanges();


            //var salesModified = db.Sales;
            //if (!String.IsNullOrEmpty(ModifyAlternateContract))
            //{
            //    foreach (string AlternateContract in salesModified)
            //    {

            //    }
            //}

            //Modifying multiple rows
            //using (db)
            //{

            //    foreach (var salesModified in db.Sales.Where(x=> x.Contract.Owner.OwnerName== searchByOwner))//.Where(x => x.LocationOfPrinterIs.Equals("U_firmi")).ToList())
            //    {
            //        salesModified.AlternateContract = ModifyAlternateContract;
            //    }
            //    db.SubmitChanges();
            //}


            return View(sales.ToList());
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