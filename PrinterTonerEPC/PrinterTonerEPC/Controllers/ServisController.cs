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
        int selectedOwnerForServis;

        // GET: Servis
        public ActionResult Index(string searchBySerialNo)
        {
            //
            //var servis = db.Servis.Include(s => s.Owner).Include(s => s.Printer).OrderBy(s=>s.Owner.OwnerName).ThenBy(o=>o.ServisDate);
            var servis = db.Servis.Include(s => s.Printer).OrderBy(s => s.Printer.Owner.OwnerName).ThenBy(o => o.ServisDate);

            if (!String.IsNullOrEmpty(searchBySerialNo))
            {
                servis = servis.Where(o => o.Printer.PrinterSerialNo.Contains(searchBySerialNo)).OrderBy(s => s.Printer.Owner.OwnerName).ThenBy(o => o.ServisDate);
            }

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

        //// GET: Servis/Create
        public ActionResult Create()
        {
            ViewBag.OwnerID = new SelectList(db.Owners, "OwnerID", "OwnerName");
            
            //ViewBag.PrinterID = new SelectList(db.Printers, "PrinterID", "PrinterSerialNo");
            ////var printersBySelectedOwner = db.Printers.Where(p => p.OwnerID == selectedOwnerForServis);
            var aaa = (int)TempData["OwnerID"];
            var printersBySelectedOwner = db.Printers.Where(p => p.OwnerID == aaa);
            
            ViewBag.PrinterID = new SelectList(printersBySelectedOwner, "PrinterID", "PrinterSerialNo");
            return View();
        }

        ////TODO: izmena
        //// GET: Servis/Create
        //public ActionResult Create()
        //{
        //    ViewBag.OwnerID = new SelectList(db.Owners, "OwnerID", "OwnerName");
        //    ViewBag.Printers = new SelectList(new List<Printer>(), "PrinterID", "PrinterSerialNo");
        //    return View();
        //}
        //public IList<Printer> GetPrinter(int id)
        //{
        //    return db.Printers.Where(p => p.OwnerID == id).ToList();
        //}

        //public JsonResult GetJsonState(int id)
        //{

        //    var printerList = this.GetPrinter(Convert.ToInt32(id));
        //    var printersList = printerList.Select(p => new SelectListItem()
        //    {
        //        Text = p.PrinterSerialNo,
        //        Value = p.PrinterID.ToString()
        //    });
            

        //    return Json(printersList, JsonRequestBehavior.AllowGet);
        //}    


        // POST: Servis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //TODO:izmena
        //public ActionResult Create([Bind(Include = "ServisID,ServisDate,ServisPrice,OwnerID,PrinterID,Napomena,Created")] Servis servis)
        public ActionResult Create([Bind(Include = "ServisID,ServisDate,ServisPrice,PrinterID,Napomena,Created")] Servis servis)
        {
            if (ModelState.IsValid)
            {
                db.Servis.Add(servis);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //TODO:izmena
            //ViewBag.OwnerID = new SelectList(db.Owners, "OwnerID", "OwnerName", servis.OwnerID);
            ViewBag.PrinterID = new SelectList(db.Printers, "PrinterID", "PrinterSerialNo", servis.PrinterID);
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
            //TODO:izmena
            //ViewBag.OwnerID = new SelectList(db.Owners, "OwnerID", "OwnerName", servis.OwnerID);
            ViewBag.PrinterID = new SelectList(db.Printers, "PrinterID", "PrinterSerialNo", servis.PrinterID);
            return View(servis);
        }

        // POST: Servis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //TODO:izmena
        //public ActionResult Edit([Bind(Include = "ServisID,ServisDate,ServisPrice,OwnerID,PrinterID,Napomena,Created")] Servis servis)
        public ActionResult Edit([Bind(Include = "ServisID,ServisDate,ServisPrice,PrinterID,Napomena,Created")] Servis servis)
        {
            if (ModelState.IsValid)
            {
                db.Entry(servis).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //TODO:izmena
            //ViewBag.OwnerID = new SelectList(db.Owners, "OwnerID", "OwnerName", servis.OwnerID);
            ViewBag.PrinterID = new SelectList(db.Printers, "PrinterID", "PrinterSerialNo", servis.PrinterID);
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

        //TODO izmena
        public ActionResult SelectOwner()
        {
            ViewBag.OwnerID = new SelectList(db.Owners, "OwnerID", "OwnerName");
            
            return View();
        }

        [HttpPost]
        public ActionResult SelectOwner(Owner model)
        {
            selectedOwnerForServis = model.OwnerID;
            TempData["OwnerID"] = model.OwnerID;
            return RedirectToAction("Create");
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
