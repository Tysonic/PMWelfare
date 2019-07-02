using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PMWelfare.Models;

namespace PMWelfare.Controllers
{
    public class sup_productsController : Controller
    {
        private welfare db = new welfare();

        // GET: sup_products
        public ActionResult Index()
        {
            var sup_products = db.sup_products.Include(s => s.celebration).Include(s => s.supplier);
            return View(sup_products.ToList());
        }

        // GET: sup_products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sup_products sup_products = db.sup_products.Find(id);
            if (sup_products == null)
            {
                return HttpNotFound();
            }
            return View(sup_products);
        }

        // GET: sup_products/Create
        public ActionResult Create()
        {
            ViewBag.event_id = new SelectList(db.celebrations, "event_id", "event_name");
            ViewBag.sup_id = new SelectList(db.suppliers, "sup_id", "sup_tel");
            return View();
        }

        // POST: sup_products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "prod_id,prod_name,price,event_id,sup_id,created_by,created_at,updated_by,updated_at")] sup_products sup_products)
        {
            if (ModelState.IsValid)
            {
                db.sup_products.Add(sup_products);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.event_id = new SelectList(db.celebrations, "event_id", "event_name", sup_products.event_id);
            ViewBag.sup_id = new SelectList(db.suppliers, "sup_id", "sup_tel", sup_products.sup_id);
            return View(sup_products);
        }

        // GET: sup_products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sup_products sup_products = db.sup_products.Find(id);
            if (sup_products == null)
            {
                return HttpNotFound();
            }
            ViewBag.event_id = new SelectList(db.celebrations, "event_id", "event_name", sup_products.event_id);
            ViewBag.sup_id = new SelectList(db.suppliers, "sup_id", "sup_tel", sup_products.sup_id);
            return View(sup_products);
        }

        // POST: sup_products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "prod_id,prod_name,price,event_id,sup_id,created_by,created_at,updated_by,updated_at")] sup_products sup_products)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sup_products).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.event_id = new SelectList(db.celebrations, "event_id", "event_name", sup_products.event_id);
            ViewBag.sup_id = new SelectList(db.suppliers, "sup_id", "sup_tel", sup_products.sup_id);
            return View(sup_products);
        }

        // GET: sup_products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sup_products sup_products = db.sup_products.Find(id);
            if (sup_products == null)
            {
                return HttpNotFound();
            }
            return View(sup_products);
        }

        // POST: sup_products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            sup_products sup_products = db.sup_products.Find(id);
            db.sup_products.Remove(sup_products);
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
