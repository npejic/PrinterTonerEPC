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

        public ActionResult ReportByOwner(string searchByOwner)//, string ModifyAlternateContract)
        {
            PrinterTonerContext db = new PrinterTonerContext();
            var sales = from s in db.Sales
                         select s;

            if (!String.IsNullOrEmpty(searchByOwner))
            {
                sales = sales.Where(s => s.Contract.Owner.OwnerName == searchByOwner && s.Printer.IsEPCprinter==true);
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