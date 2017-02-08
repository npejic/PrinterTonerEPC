﻿using System;
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
            var sales = db.Sales.Include(s => s.Contract).Include(s => s.Printer).Include(s => s.Toner);
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

        // GET: Sales/Create
        public ActionResult Create()
        {
            ViewBag.ContractID = new SelectList(db.Contracts, "ContractID", "ContractName");
            ViewBag.PrinterID = new SelectList(db.Printers, "PrinterID", "PrinterInternalNo");
            ViewBag.TonerID = new SelectList(db.Toners, "TonerID", "TonerModel");
            return View();
        }

        // POST: Sales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SaleID,SaleDate,Price,LocationOfPrinterIs,ContractID,PrinterID,TonerID,Created")] Sale sale)
        {
            if (ModelState.IsValid)
            {
                db.Sales.Add(sale);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ContractID = new SelectList(db.Contracts, "ContractID", "ContractName", sale.ContractID);
            ViewBag.PrinterID = new SelectList(db.Printers, "PrinterID", "PrinterInternalNo", sale.PrinterID);
            ViewBag.TonerID = new SelectList(db.Toners, "TonerID", "TonerModel", sale.TonerID);
            return View(sale);
        }

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
            ViewBag.TonerID = new SelectList(db.Toners, "TonerID", "TonerModel", sale.TonerID);
            return View(sale);
        }

        // POST: Sales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SaleID,SaleDate,Price,LocationOfPrinterIs,ContractID,PrinterID,TonerID,Created")] Sale sale)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sale).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ContractID = new SelectList(db.Contracts, "ContractID", "ContractName", sale.ContractID);
            ViewBag.PrinterID = new SelectList(db.Printers, "PrinterID", "PrinterInternalNo", sale.PrinterID);
            ViewBag.TonerID = new SelectList(db.Toners, "TonerID", "TonerModel", sale.TonerID);
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
