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
    public class member_statusController : Controller
    {
        private welfare db = new welfare();

        // GET: member_status
        public ActionResult Index()
        {
            return View(db.member_status.ToList());
        }

        // GET: member_status/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            member_status member_status = db.member_status.Find(id);
            if (member_status == null)
            {
                return HttpNotFound();
            }
            return View(member_status);
        }

        // GET: member_status/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: member_status/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,member_status1,created_by,created_at,updated_by,updated_at")] member_status member_status)
        {
            if (ModelState.IsValid)
            {
                db.member_status.Add(member_status);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(member_status);
        }

        // GET: member_status/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            member_status member_status = db.member_status.Find(id);
            if (member_status == null)
            {
                return HttpNotFound();
            }
            return View(member_status);
        }

        // POST: member_status/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,member_status1,created_by,created_at,updated_by,updated_at")] member_status member_status)
        {
            if (ModelState.IsValid)
            {
                db.Entry(member_status).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(member_status);
        }

        // GET: member_status/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            member_status member_status = db.member_status.Find(id);
            if (member_status == null)
            {
                return HttpNotFound();
            }
            return View(member_status);
        }

        // POST: member_status/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            member_status member_status = db.member_status.Find(id);
            db.member_status.Remove(member_status);
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
