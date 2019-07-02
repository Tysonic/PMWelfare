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
    public class expensController : Controller
    {
        private welfare db = new welfare();

        // GET: expens
        public ActionResult Index()
        {
            var expenses = db.expenses.Include(e => e.sup_products);
            return View(expenses.ToList());
        }

        // GET: expens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            expens expens = db.expenses.Find(id);
            if (expens == null)
            {
                return HttpNotFound();
            }
            return View(expens);
        }

        // GET: expens/Create
        public ActionResult Create()
        {
            ViewBag.prod_id = new SelectList(db.sup_products, "prod_id", "prod_name");
            return View();
        }

        // POST: expens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "exp_id,exp_date,prod_id,quantity,created_by,created_at,updated_by,updated_at")] expens expens)
        {
            if (ModelState.IsValid)
            {
                db.expenses.Add(expens);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.prod_id = new SelectList(db.sup_products, "prod_id", "prod_name", expens.prod_id);
            return View(expens);
        }

        // GET: expens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            expens expens = db.expenses.Find(id);
            if (expens == null)
            {
                return HttpNotFound();
            }
            ViewBag.prod_id = new SelectList(db.sup_products, "prod_id", "prod_name", expens.prod_id);
            return View(expens);
        }

        // POST: expens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "exp_id,exp_date,prod_id,quantity,created_by,created_at,updated_by,updated_at")] expens expens)
        {
            if (ModelState.IsValid)
            {
                db.Entry(expens).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.prod_id = new SelectList(db.sup_products, "prod_id", "prod_name", expens.prod_id);
            return View(expens);
        }

        // GET: expens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            expens expens = db.expenses.Find(id);
            if (expens == null)
            {
                return HttpNotFound();
            }
            return View(expens);
        }

        // POST: expens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            expens expens = db.expenses.Find(id);
            db.expenses.Remove(expens);
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
