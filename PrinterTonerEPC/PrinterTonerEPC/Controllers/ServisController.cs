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
    public class ServisController : Controller
    {
        private PrinterTonerContext db = new PrinterTonerContext();

        // GET: Servis
        public ActionResult Index()
        {
            var servis = db.Servis.Include(s => s.Owner).Include(s => s.Printer);
            return View(servis.ToList());
        }

        // GET: Servis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servis servis = db.Servis.Find(id);
            if (servis == null)
            {
                return HttpNotFound();
            }
            return View(servis);
        }

        // GET: Servis/Create
        public ActionResult Create()
        {
            ViewBag.OwnerID = new SelectList(db.Owners, "OwnerID", "OwnerName");
            ViewBag.PrinterID = new SelectList(db.Printers, "PrinterID", "PrinterInternalNo");
            return View();
        }

        // POST: Servis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ServisID,ServisDate,OwnerID,PrinterID,Napomena,Created")] Servis servis)
        {
            if (ModelState.IsValid)
            {
                db.Servis.Add(servis);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OwnerID = new SelectList(db.Owners, "OwnerID", "OwnerName", servis.OwnerID);
            ViewBag.PrinterID = new SelectList(db.Printers, "PrinterID", "PrinterInternalNo", servis.PrinterID);
            return View(servis);
        }

        // GET: Servis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servis servis = db.Servis.Find(id);
            if (servis == null)
            {
                return HttpNotFound();
            }
            ViewBag.OwnerID = new SelectList(db.Owners, "OwnerID", "OwnerName", servis.OwnerID);
            ViewBag.PrinterID = new SelectList(db.Printers, "PrinterID", "PrinterInternalNo", servis.PrinterID);
            return View(servis);
        }

        // POST: Servis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ServisID,ServisDate,OwnerID,PrinterID,Napomena,Created")] Servis servis)
        {
            if (ModelState.IsValid)
            {
                db.Entry(servis).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OwnerID = new SelectList(db.Owners, "OwnerID", "OwnerName", servis.OwnerID);
            ViewBag.PrinterID = new SelectList(db.Printers, "PrinterID", "PrinterInternalNo", servis.PrinterID);
            return View(servis);
        }

        // GET: Servis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servis servis = db.Servis.Find(id);
            if (servis == null)
            {
                return HttpNotFound();
            }
            return View(servis);
        }

        // POST: Servis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Servis servis = db.Servis.Find(id);
            db.Servis.Remove(servis);
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
