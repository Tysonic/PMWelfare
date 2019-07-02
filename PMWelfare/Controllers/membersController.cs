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
    public class membersController : Controller
    {
        private welfare db = new welfare();

        // GET: members
        public ActionResult Index()
        {
            var members = db.members.SqlQuery("select * from members where user_name = 'nicho'");
            return View(members.ToList());
        }


        // GET: members/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            member member = db.members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // GET: members/Create
        public ActionResult Create()
        {
            ViewBag.member_status = new SelectList(db.member_status, "id", "member_status1");
            return View();
        }

        // POST: members/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "user_name,first_name,other_name,email,dob,member_status,is_admin,created_by,created_at,updated_by,updated_at")] member member)
        {
            if (ModelState.IsValid)
            {
                db.members.Add(member);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.member_status = new SelectList(db.member_status, "id", "member_status1", member.member_status);
            return View(member);
        }

        // GET: members/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            member member = db.members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            ViewBag.member_status = new SelectList(db.member_status, "id", "member_status1", member.member_status);
            return View(member);
        }

        // POST: members/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "user_name,first_name,other_name,email,dob,member_status,is_admin,created_by,created_at,updated_by,updated_at")] member member)
        {
            if (ModelState.IsValid)
            {
                db.Entry(member).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.member_status = new SelectList(db.member_status, "id", "member_status1", member.member_status);
            return View(member);
        }

        // GET: members/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            member member = db.members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            member member = db.members.Find(id);
            db.members.Remove(member);
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
