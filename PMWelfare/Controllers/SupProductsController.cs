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
    public class SupProductsController : Controller
    {
        private welfare db = new welfare();

        // GET: SupProducts
        public ActionResult Index()
        {
            var supProducts = db.SupProducts.Include(s => s.Celebration).Include(s => s.Supplier);
            return View(supProducts.ToList());
        }

        // GET: SupProducts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupProducts supProducts = db.SupProducts.Find(id);
            if (supProducts == null)
            {
                return HttpNotFound();
            }
            return View(supProducts);
        }

        // GET: SupProducts/Create
        public ActionResult Create()
        {
            ViewBag.EventId = new SelectList(db.Celebrations, "EventId", "EventType");
            ViewBag.SupId = new SelectList(db.Suppliers, "SupId", "SupTel");
            return View();
        }

        // POST: SupProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductName," +
            "UnitPrice,EventId,SupId,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt")] SupProducts supProducts)
        {
            if (ModelState.IsValid)
            {
                db.SupProducts.Add(supProducts);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EventId = new SelectList(db.Celebrations, "EventId", "EventType.Type", 
                supProducts.EventId);
            ViewBag.SupId = new SelectList(db.Suppliers, "SupId", "SupTel",
                supProducts.SupId);
            return View(supProducts);
        }

        // GET: SupProducts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupProducts supProducts = db.SupProducts.Find(id);
            if (supProducts == null)
            {
                return HttpNotFound();
            }
            ViewBag.EventId = new SelectList(db.Celebrations, "EventId", "EventType.Type",
                supProducts.EventId);
            ViewBag.SupId = new SelectList(db.Suppliers, "SupId",
                "SupTel", supProducts.SupId);
            return View(supProducts);
        }

        // POST: SupProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,ProductName,UnitPrice," +
            "EventId,SupId,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt")] SupProducts supProducts)
        {
            if (ModelState.IsValid)
            {
                db.Entry(supProducts).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EventId = new SelectList(db.Celebrations,
                "EventId", "EventType.type", supProducts.EventId);
            ViewBag.SupId = new SelectList(db.Suppliers, "SupId", 
                "SupTel", supProducts.SupId);
            return View(supProducts);
        }

        // GET: SupProducts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupProducts supProducts = db.SupProducts.Find(id);
            if (supProducts == null)
            {
                return HttpNotFound();
            }
            return View(supProducts);
        }

        // POST: SupProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SupProducts supProducts = db.SupProducts.Find(id);
            db.SupProducts.Remove(supProducts);
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
