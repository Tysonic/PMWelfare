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
    public class monthly_summaryController : Controller
    {
        private welfare db = new welfare();

        // GET: monthly_summary
        public ActionResult Index()
        {
            return View(db.monthly_summary.ToList());
        }

        // GET: monthly_summary/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            monthly_summary monthly_summary = db.monthly_summary.Find(id);
            if (monthly_summary == null)
            {
                return HttpNotFound();
            }
            return View(monthly_summary);
        }

        // GET: monthly_summary/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: monthly_summary/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,start_at,end_at,closing_balance,created_by,created_at,updated_by,updated_at")] monthly_summary monthly_summary)
        {
            if (ModelState.IsValid)
            {
                db.monthly_summary.Add(monthly_summary);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(monthly_summary);
        }

        // GET: monthly_summary/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            monthly_summary monthly_summary = db.monthly_summary.Find(id);
            if (monthly_summary == null)
            {
                return HttpNotFound();
            }
            return View(monthly_summary);
        }

        // POST: monthly_summary/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,start_at,end_at,closing_balance,created_by,created_at,updated_by,updated_at")] monthly_summary monthly_summary)
        {
            if (ModelState.IsValid)
            {
                db.Entry(monthly_summary).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(monthly_summary);
        }

        // GET: monthly_summary/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            monthly_summary monthly_summary = db.monthly_summary.Find(id);
            if (monthly_summary == null)
            {
                return HttpNotFound();
            }
            return View(monthly_summary);
        }

        // POST: monthly_summary/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            monthly_summary monthly_summary = db.monthly_summary.Find(id);
            db.monthly_summary.Remove(monthly_summary);
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
