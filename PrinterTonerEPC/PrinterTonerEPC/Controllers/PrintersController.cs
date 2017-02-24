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
    public class PrintersController : Controller
    {
        private PrinterTonerContext db = new PrinterTonerContext();

        // GET: Printers
        public ActionResult Index()
        {
            var sortedPrinters = db.Printers.OrderBy(p => p.PrinterManufacturer).ThenBy(p => p.PrinterModel).ThenBy(p => p.PrinterSerialNo);
            return View(sortedPrinters.ToList());
        }

        // GET: Printers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Printer printer = db.Printers.Find(id);
            if (printer == null)
            {
                return HttpNotFound();
            }
            return View(printer);
        }

        // GET: Printers/Create
        public ActionResult Create()
        {
            //TODO:izmena
            ViewBag.OwnerID = new SelectList(db.Owners, "OwnerID", "OwnerName");
            return View();
        }

        // POST: Printers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PrinterID,OwnerID,PrinterInternalNo,PrinterManufacturer,PrinterModel,PrinterSerialNo,PrinterHardwareNo,PrinterDecommissioned,IsEPCprinter,Created")] Printer printer)
        {
            if (ModelState.IsValid)
            {
                db.Printers.Add(printer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(printer);
        }

        // GET: Printers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Printer printer = db.Printers.Find(id);
            if (printer == null)
            {
                return HttpNotFound();
            }
            //TODO:izmena
            ViewBag.OwnerID = new SelectList(db.Owners, "OwnerID", "OwnerName", printer.OwnerID);
            return View(printer);
        }

        // POST: Printers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PrinterID,OwnerID,PrinterInternalNo,PrinterManufacturer,PrinterModel,PrinterSerialNo,PrinterHardwareNo,PrinterDecommissioned,IsEPCprinter,Created")] Printer printer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(printer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //TODO:izmena
            ViewBag.OwnerID = new SelectList(db.Owners, "OwnerID", "OwnerName", printer.OwnerID);
            return View(printer);
        }

        // GET: Printers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Printer printer = db.Printers.Find(id);
            if (printer == null)
            {
                return HttpNotFound();
            }
            return View(printer);
        }

        // POST: Printers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Printer printer = db.Printers.Find(id);
            db.Printers.Remove(printer);
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
