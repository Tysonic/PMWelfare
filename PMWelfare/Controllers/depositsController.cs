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
    public class depositsController : Controller
    {
        private welfare db = new welfare();

        // GET: deposits
        public ActionResult Index()
        {
            var deposits = db.deposits.Include(s => s.member);
            return View(deposits.ToList());
        }
          
        //public ActionResult Collections()
        //{
        //    var coll = db.deposits.SqlQuery("select *  from deposits ");

        //    return View(coll.ToList());
        //}

        // GET: deposits/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            deposit deposit = db.deposits.Find(id);
            if (deposit == null)
            {
                return HttpNotFound();
            }
            return View(deposit);
        }

        // GET: deposits/Create
        public ActionResult Create()
        {
            ViewBag.user_name = new SelectList(db.members, "user_name", "first_name");
            return View();
        }

        // POST: deposits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "dep_id,user_name,amount,created_by,created_at,updated_by,updated_at")] deposit deposit)
        {
            if (ModelState.IsValid)
            {
                db.deposits.Add(deposit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.user_name = new SelectList(db.members, "user_name", "first_name", deposit.user_name);
            return View(deposit);
        }

        // GET: deposits/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            deposit deposit = db.deposits.Find(id);
            if (deposit == null)
            {
                return HttpNotFound();
            }
            ViewBag.user_name = new SelectList(db.members, "user_name", "first_name", deposit.user_name);
            return View(deposit);
        }

        // POST: deposits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "dep_id,user_name,amount,created_by,created_at,updated_by,updated_at")] deposit deposit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(deposit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.user_name = new SelectList(db.members, "user_name", "first_name", deposit.user_name);
            return View(deposit);
        }

        // GET: deposits/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            deposit deposit = db.deposits.Find(id);
            if (deposit == null)
            {
                return HttpNotFound();
            }
            return View(deposit);
        }

        // POST: deposits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            deposit deposit = db.deposits.Find(id);
            db.deposits.Remove(deposit);
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
