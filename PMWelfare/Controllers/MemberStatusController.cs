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
    public class MemberStatusController : Controller
    {
        private welfare db = new welfare();

        // GET: MemberStatus
        public ActionResult Index()
        {
            return View(db.MemberStatus.ToList());
        }

        // GET: MemberStatus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberStatus memberStatus = db.MemberStatus.Find(id);
            if (memberStatus == null)
            {
                return HttpNotFound();
            }
            return View(memberStatus);
        }

        // GET: MemberStatus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MemberStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MembersStatusID,MemberStatus1,CreatedBy,CreatedAt,TimeStamp,UpdatedBy,UpdatedAt")] MemberStatus memberStatus)
        {
            if (ModelState.IsValid)
            {
                memberStatus.CreatedBy = "Timo";
                db.MemberStatus.Add(memberStatus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(memberStatus);
        }

        // GET: MemberStatus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberStatus memberStatus = db.MemberStatus.Find(id);
            if (memberStatus == null)
            {
                return HttpNotFound();
            }
            return View(memberStatus);
        }

        // POST: MemberStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MembersStatusID,MemberStatus1,CreatedBy,CreatedAt,TimeStamp,UpdatedBy,UpdatedAt")] MemberStatus memberStatus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(memberStatus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(memberStatus);
        }

        // GET: MemberStatus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberStatus memberStatus = db.MemberStatus.Find(id);
            if (memberStatus == null)
            {
                return HttpNotFound();
            }
            return View(memberStatus);
        }

        // POST: MemberStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MemberStatus memberStatus = db.MemberStatus.Find(id);
            db.MemberStatus.Remove(memberStatus);
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
