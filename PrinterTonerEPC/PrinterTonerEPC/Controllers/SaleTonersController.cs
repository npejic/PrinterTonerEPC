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
    public class SaleTonersController : Controller
    {
        private PrinterTonerContext db = new PrinterTonerContext();

        // GET: SaleToners
        public ActionResult Index()
        {
            var saleToners = db.SaleToners.Include(s => s.Contract).Include(s => s.Toner)
                                .OrderBy(s => s.Contract.ContractName).ThenBy(s => s.Toner.TonerModel).ThenBy(s => s.SaleTonerDate);
            
            

            
            return View(saleToners.ToList());
        }

        //Returns list of owners (companies) that didn't order toners in last X (periodInMonths) months
        public ActionResult TonerAlarm(string periodInMonths)
        {

            var ownersWithNoAlarmOrder = db.SaleToners.GroupBy(g => new { g.Contract.Owner.OwnerName, g.TonerID }).Select(s => s.OrderByDescending(x => x.SaleTonerDate).FirstOrDefault()).OrderBy(s => s.Contract.Owner.OwnerName).ThenBy(s => s.Toner.TonerModel);

            if (!String.IsNullOrEmpty(periodInMonths))
            {
                int period = Int16.Parse(periodInMonths);
                var LimitDate = DateTime.Now.Date;
                LimitDate = LimitDate.AddMonths(-period);
                ownersWithNoAlarmOrder = ownersWithNoAlarmOrder.Where(o => o.SaleTonerDate < LimitDate).OrderBy(s => s.Contract.ContractName).ThenBy(s => s.Toner.TonerModel).ThenBy(s => s.SaleTonerDate);
            }

            return View(ownersWithNoAlarmOrder.ToList());
        }


        //LastTonerSale
        public ActionResult LastTonerSale(string searchByOwner, string searchByToner)
        {
            
            ///OVO RADI!!! treba samo da zanemari koji je ugovor u pitanju i prikaže samo vlasnika
            //var lastTonerSale = db.SaleToners.GroupBy(g => new { g.ContractID, g.TonerID }).Select(s=>s.OrderByDescending(x=>x.SaleTonerDate).FirstOrDefault());

            var lastTonerSale = db.SaleToners.GroupBy(g => new { g.Contract.Owner.OwnerName, g.TonerID }).Select(s => s.OrderByDescending(x => x.SaleTonerDate).FirstOrDefault()).OrderBy(s=>s.Contract.Owner.OwnerName).ThenBy(s=>s.Toner.TonerModel);

            if (!String.IsNullOrEmpty(searchByOwner))
            {
                lastTonerSale = lastTonerSale.Where(o => o.Contract.Owner.OwnerName.Contains(searchByOwner)).OrderBy(s => s.Contract.ContractName).ThenBy(s => s.Toner.TonerModel).ThenBy(s => s.SaleTonerDate);
            }


            if (!String.IsNullOrEmpty(searchByToner))
            {
                lastTonerSale = lastTonerSale.Where(o => o.Toner.TonerModel.Contains(searchByToner)).OrderBy(s => s.Contract.ContractName).ThenBy(s => s.Toner.TonerModel).ThenBy(s => s.SaleTonerDate);
            }

            return View(lastTonerSale.ToList());
        }


        // GET: SaleToners/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SaleToner saleToner = db.SaleToners.Find(id);
            if (saleToner == null)
            {
                return HttpNotFound();
            }
            return View(saleToner);
        }

        // GET: SaleToners/Create
        public ActionResult Create()
        {
            ViewBag.ContractID = new SelectList(db.Contracts, "ContractID", "ContractName");
            ViewBag.TonerID = new SelectList(db.Toners, "TonerID", "TonerModel");
            return View();
        }

        // POST: SaleToners/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SaleTonerID,SaleTonerDate,TonerPrice,ContractID,TonerID")] SaleToner saleToner)
        {
            if (ModelState.IsValid)
            {
                db.SaleToners.Add(saleToner);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ContractID = new SelectList(db.Contracts, "ContractID", "ContractName", saleToner.ContractID);
            ViewBag.TonerID = new SelectList(db.Toners, "TonerID", "TonerModel", saleToner.TonerID);
            return View(saleToner);
        }

        // GET: SaleToners/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SaleToner saleToner = db.SaleToners.Find(id);
            if (saleToner == null)
            {
                return HttpNotFound();
            }
            ViewBag.ContractID = new SelectList(db.Contracts, "ContractID", "ContractName", saleToner.ContractID);
            ViewBag.TonerID = new SelectList(db.Toners, "TonerID", "TonerModel", saleToner.TonerID);
            return View(saleToner);
        }

        // POST: SaleToners/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SaleTonerID,SaleTonerDate,TonerPrice,ContractID,TonerID")] SaleToner saleToner)
        {
            if (ModelState.IsValid)
            {
                db.Entry(saleToner).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ContractID = new SelectList(db.Contracts, "ContractID", "ContractName", saleToner.ContractID);
            ViewBag.TonerID = new SelectList(db.Toners, "TonerID", "TonerModel", saleToner.TonerID);
            return View(saleToner);
        }

        // GET: SaleToners/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SaleToner saleToner = db.SaleToners.Find(id);
            if (saleToner == null)
            {
                return HttpNotFound();
            }
            return View(saleToner);
        }

        // POST: SaleToners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SaleToner saleToner = db.SaleToners.Find(id);
            db.SaleToners.Remove(saleToner);
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
