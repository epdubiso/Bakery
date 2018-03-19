using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bakery2018.Models;

namespace Bakery2018.Controllers
{
    public class SalesController : Controller
    {
        private BakeryEntities db = new BakeryEntities();

        // GET: Sales
        public ActionResult Index()
        {
            var sales = db.Sales.Include(s => s.Employee).Include(s => s.Person).Include(s => s.Product);
        
            return View(db.Sales.ToList());
        }

        // GET: Sales/Receipt/5
        public ActionResult Receipt(int? id)
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
            ViewBag.ProductKey= new SelectList(db.Products, "ProductKey", "ProductName");
            ViewBag.EmployeeKey = new SelectList(db.Employees, "EmployeeKey", "EmployeeTitle");
            ViewBag.CustomerKey = new SelectList(db.People, "PersonKey", "PersonLastName");
            return View();
        }

        // POST: Sales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductKey,ProductName,ProductPrice,SaleKey,SaleDate,CustomerKey,EmployeeKey")] Sale sale)
        {
            if (ModelState.IsValid)
            {
                db.Sales.Add(sale);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductKey = new SelectList(db.Sales, "ProductKey", "ProductName", sale.ProductKey);
            ViewBag.EmployeeKey = new SelectList(db.Employees, "EmployeeKey", "EmployeeTitle", sale.EmployeeKey);
            ViewBag.CustomerKey = new SelectList(db.People, "PersonKey", "PersonLastName", sale.CustomerKey);
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
            ViewBag.ProductKey = new SelectList(db.Sales, "ProductKey", "ProductName", sale.ProductKey);
            ViewBag.EmployeeKey = new SelectList(db.Employees, "EmployeeKey", "EmployeeTitle", sale.EmployeeKey);
            ViewBag.CustomerKey = new SelectList(db.People, "PersonKey", "PersonLastName", sale.CustomerKey);
            return View(sale);
        }

        // POST: Sales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductKey,ProductName,ProductPrice,SaleKey,SaleDate,CustomerKey,EmployeeKey")] Sale sale)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sale).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductKey = new SelectList(db.Sales, "ProductKey", "ProductName", sale.ProductKey);
            ViewBag.EmployeeKey = new SelectList(db.Employees, "EmployeeKey", "EmployeeTitle", sale.EmployeeKey);
            ViewBag.CustomerKey = new SelectList(db.People, "PersonKey", "PersonLastName", sale.CustomerKey);
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
