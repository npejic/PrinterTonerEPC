using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PrinterToner.Models;
using PrinterTonerEPC.DAL;

namespace PrinterTonerEPC.Controllers
{
    public class SalesController : Controller
    {
        private PrinterTonerContext db = new PrinterTonerContext();

        // GET: Sales
        public ActionResult Index()
        {
            var sales = db.Sales.Include(s => s.Contract).Include(s => s.Printer).OrderBy(s => s.Contract.ContractName).ThenBy(s => s.Contract.ContractDate);
            return View(sales.ToList());
        }

        public ActionResult SalesReportByOwner(string searchByOwner)
        {
            PrinterTonerContext db = new PrinterTonerContext();
            var sales = db.Sales.Where(s=>s.Printer.IsEPCprinter==true).OrderBy(s => s.Contract.Owner.OwnerName).ThenBy(s => s.Contract.ContractName);

            if (!string.IsNullOrEmpty(searchByOwner))
            {
                sales = sales.Where(s => s.Contract.Owner.OwnerName.Contains(searchByOwner)).OrderBy(s => s.Contract.Owner.OwnerName).ThenBy(s => s.Contract.ContractName);// && s.printer.isepcprinter==true);
            }

            return View(sales.ToList());
        }

        // GET: Sales/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = db.Sales.Find(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            return View(sale);
        }

        // GET: Sales/CreateByOwner
        public ActionResult CreateByOwner()
        {
            
            ViewBag.PrinterID = new SelectList(db.Printers, "PrinterID", "PrinterSerialNo");

            return View();
        }

        // GET: Sales/Create
        public ActionResult Create()
        {
            var printersNotOwnedByEPC = db.Printers.Where(p => p.Owner.OwnerName == "EPC DOO");
            ViewBag.ContractID = new SelectList(db.Contracts, "ContractID", "ContractName");
            //ViewBag.PrinterID = new SelectList(db.Printers, "PrinterID", "PrinterSerialNo");
            ViewBag.PrinterID = new SelectList(printersNotOwnedByEPC, "PrinterID", "PrinterSerialNo");
            
            return View();
        }

        // POST: Sales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SaleID,SaleDate,Price,LocationOfPrinterIs,ContractID,AlternateContract,PrinterID,TonerID,Created")] Sale sale)
        {
            if (ModelState.IsValid)
            {
                db.Sales.Add(sale);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ContractID = new SelectList(db.Contracts, "ContractID", "ContractName", sale.ContractID);
            ViewBag.PrinterID = new SelectList(db.Printers, "PrinterID", "PrinterInternalNo", sale.PrinterID);
            return View(sale);
        }

        //TODO:
        // GET: Sales/Edit/5
        public ActionResult EditSalesReportByOwner(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = db.Sales.Find(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            ViewBag.ContractID = new SelectList(db.Contracts, "ContractID", "ContractName", sale.ContractID);
            ViewBag.PrinterID = new SelectList(db.Printers, "PrinterID", "PrinterInternalNo", sale.PrinterID);
            
            return View(sale);
        }

        // POST: Sales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSalesReportByOwner([Bind(Include = "SaleID,SaleDate,Price,LocationOfPrinterIs,ContractID,AlternateContract,PrinterID,TonerID,Created")] Sale sale)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sale).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("SalesReportByOwner");
            }
            ViewBag.ContractID = new SelectList(db.Contracts, "ContractID", "ContractName", sale.ContractID);
            ViewBag.PrinterID = new SelectList(db.Printers, "PrinterID", "PrinterInternalNo", sale.PrinterID);
           
            return View(sale);
        }

        ////////////////////////////////
        
        
        
        // GET: Sales/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = db.Sales.Find(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            ViewBag.ContractID = new SelectList(db.Contracts, "ContractID", "ContractName", sale.ContractID);
            ViewBag.PrinterID = new SelectList(db.Printers, "PrinterID", "PrinterInternalNo", sale.PrinterID);
            
            return View(sale);
        }

        // POST: Sales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SaleID,SaleDate,Price,LocationOfPrinterIs,ContractID,AlternateContract,PrinterID,TonerID,Created")] Sale sale)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sale).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ContractID = new SelectList(db.Contracts, "ContractID", "ContractName", sale.ContractID);
            ViewBag.PrinterID = new SelectList(db.Printers, "PrinterID", "PrinterInternalNo", sale.PrinterID);
            
            return View(sale);
        }

        // GET: Sales/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = db.Sales.Find(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            return View(sale);
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sale sale = db.Sales.Find(id);
            db.Sales.Remove(sale);
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
