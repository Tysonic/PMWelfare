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
    public class subscriptionsController : Controller
    {
        private welfare db = new welfare();

        // GET: subscriptions
        public ActionResult Index()
        {
            var subscriptions = db.subscriptions.Include(s => s.member);
            return View(subscriptions.ToList());
        }

        // GET: subscriptions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            subscription subscription = db.subscriptions.Find(id);
            if (subscription == null)
            {
                return HttpNotFound();
            }
            return View(subscription);
        }

        // GET: subscriptions/Create
        public ActionResult Create()
        {
            ViewBag.user_name = new SelectList(db.members, "user_name", "first_name");
            return View();
        }

        // POST: subscriptions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "sub_id,user_name,amount,sub_month,sub_year,created_by,created_at,updated_by,updated_at")] subscription subscription)
        {
            if (ModelState.IsValid)
            {
                db.subscriptions.Add(subscription);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.user_name = new SelectList(db.members, "user_name", "first_name", subscription.user_name);
            return View(subscription);
        }

        // GET: subscriptions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            subscription subscription = db.subscriptions.Find(id);
            if (subscription == null)
            {
                return HttpNotFound();
            }
            ViewBag.user_name = new SelectList(db.members, "user_name", "first_name", subscription.user_name);
            return View(subscription);
        }

        // POST: subscriptions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "sub_id,user_name,amount,sub_month,sub_year,created_by,created_at,updated_by,updated_at")] subscription subscription)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subscription).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.user_name = new SelectList(db.members, "user_name", "first_name", subscription.user_name);
            return View(subscription);
        }

        // GET: subscriptions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            subscription subscription = db.subscriptions.Find(id);
            if (subscription == null)
            {
                return HttpNotFound();
            }
            return View(subscription);
        }

        // POST: subscriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            subscription subscription = db.subscriptions.Find(id);
            db.subscriptions.Remove(subscription);
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
