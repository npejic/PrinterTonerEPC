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
    public class PrinterTonerCompatibilitiesController : Controller
    {
        private PrinterTonerContext db = new PrinterTonerContext();

        // GET: PrinterTonerCompatibilities
        public ActionResult Index(string searchByPrinter, string searchByToner)
        {
            var printerTonerCompatibilitys = db.PrinterTonerCompatibilitys.Include(p => p.Printer).Include(p => p.Toner).OrderBy(p=>p.Printer.PrinterManufacturer).ThenBy(p=>p.Printer.PrinterModel);

            if (!String.IsNullOrEmpty(searchByPrinter))
            {
                printerTonerCompatibilitys = printerTonerCompatibilitys.Where(o => o.Printer.PrinterModel.Contains(searchByPrinter)).OrderBy(p => p.Printer.PrinterManufacturer).ThenBy(p => p.Printer.PrinterModel);
            }


            if (!String.IsNullOrEmpty(searchByToner))
            {
                printerTonerCompatibilitys = printerTonerCompatibilitys.Where(o => o.Toner.TonerModel.Contains(searchByToner)).OrderBy(p => p.Printer.PrinterManufacturer).ThenBy(p => p.Printer.PrinterModel);
            }

            return View(printerTonerCompatibilitys.ToList());
        }

        // GET: PrinterTonerCompatibilities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrinterTonerCompatibility printerTonerCompatibility = db.PrinterTonerCompatibilitys.Find(id);
            if (printerTonerCompatibility == null)
            {
                return HttpNotFound();
            }
            return View(printerTonerCompatibility);
        }

        // GET: PrinterTonerCompatibilities/Create
        public ActionResult Create()
        {
            //da se ne bi pojavljivali duplikati iz PrinterModela
            var printerModelNoDuplicate = db.Printers.GroupBy(s => s.PrinterModel).Select(x => x.FirstOrDefault());//.OrderBy(s => s.PrinterModel).Select(s => new { s.PrinterID, s.PrinterModel }).Distinct();
            ViewBag.PrinterID = new SelectList(printerModelNoDuplicate, "PrinterID", "PrinterModel");

            //
            //ViewBag.PrinterID = new SelectList(db.Printers, "PrinterID", "PrinterModel");
            
            ViewBag.TonerID = new SelectList(db.Toners, "TonerID", "TonerModel");
            return View();
        }

        // POST: PrinterTonerCompatibilities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PrinterTonerCompatibilityID,PrinterID,TonerID")] PrinterTonerCompatibility printerTonerCompatibility)
        {
            if (ModelState.IsValid)
            {
                db.PrinterTonerCompatibilitys.Add(printerTonerCompatibility);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //var printerModelNoDuplicate = db.Printers.GroupBy(s => s.PrinterModel).Select(x => x.FirstOrDefault());//.OrderBy(s => s.PrinterModel).Select(s => new { s.PrinterID, s.PrinterModel }).Distinct();
            //ViewBag.PrinterID = new SelectList(printerModelNoDuplicate);

            ViewBag.PrinterID = new SelectList(db.Printers, "PrinterID", "PrinterModel", printerTonerCompatibility.PrinterID);
            ViewBag.TonerID = new SelectList(db.Toners, "TonerID", "TonerModel", printerTonerCompatibility.TonerID);
            return View(printerTonerCompatibility);
        }

        // GET: PrinterTonerCompatibilities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrinterTonerCompatibility printerTonerCompatibility = db.PrinterTonerCompatibilitys.Find(id);
            if (printerTonerCompatibility == null)
            {
                return HttpNotFound();
            }
            ViewBag.PrinterID = new SelectList(db.Printers, "PrinterID", "PrinterModel", printerTonerCompatibility.PrinterID);
            ViewBag.TonerID = new SelectList(db.Toners, "TonerID", "TonerModel", printerTonerCompatibility.TonerID);
            return View(printerTonerCompatibility);
        }

        // POST: PrinterTonerCompatibilities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PrinterTonerCompatibilityID,PrinterID,TonerID")] PrinterTonerCompatibility printerTonerCompatibility)
        {
            if (ModelState.IsValid)
            {
                db.Entry(printerTonerCompatibility).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PrinterID = new SelectList(db.Printers, "PrinterID", "PrinterModel", printerTonerCompatibility.PrinterID);
            ViewBag.TonerID = new SelectList(db.Toners, "TonerID", "TonerModel", printerTonerCompatibility.TonerID);
            return View(printerTonerCompatibility);
        }

        // GET: PrinterTonerCompatibilities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrinterTonerCompatibility printerTonerCompatibility = db.PrinterTonerCompatibilitys.Find(id);
            if (printerTonerCompatibility == null)
            {
                return HttpNotFound();
            }
            return View(printerTonerCompatibility);
        }

        // POST: PrinterTonerCompatibilities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PrinterTonerCompatibility printerTonerCompatibility = db.PrinterTonerCompatibilitys.Find(id);
            db.PrinterTonerCompatibilitys.Remove(printerTonerCompatibility);
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
