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
    public class chat_roomController : Controller
    {
        private welfare db = new welfare();

        // GET: chat_room
        public ActionResult Index()
        {
            var chat_room = db.chat_room.Include(c => c.member);
            return View(chat_room.ToList());
        }

        // GET: chat_room/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            chat_room chat_room = db.chat_room.Find(id);
            if (chat_room == null)
            {
                return HttpNotFound();
            }
            return View(chat_room);
        }

        // GET: chat_room/Create
        public ActionResult Create()
        {
            ViewBag.user_name = new SelectList(db.members, "user_name", "first_name");
            return View();
        }

        // POST: chat_room/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "chat_id,user_name,message,posted_at,updated_by,updated_at")] chat_room chat_room)
        {
            if (ModelState.IsValid)
            {
                db.chat_room.Add(chat_room);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.user_name = new SelectList(db.members, "user_name", "first_name", chat_room.user_name);
            return View(chat_room);
        }

        // GET: chat_room/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            chat_room chat_room = db.chat_room.Find(id);
            if (chat_room == null)
            {
                return HttpNotFound();
            }
            ViewBag.user_name = new SelectList(db.members, "user_name", "first_name", chat_room.user_name);
            return View(chat_room);
        }

        // POST: chat_room/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "chat_id,user_name,message,posted_at,updated_by,updated_at")] chat_room chat_room)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chat_room).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.user_name = new SelectList(db.members, "user_name", "first_name", chat_room.user_name);
            return View(chat_room);
        }

        // GET: chat_room/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            chat_room chat_room = db.chat_room.Find(id);
            if (chat_room == null)
            {
                return HttpNotFound();
            }
            return View(chat_room);
        }

        // POST: chat_room/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            chat_room chat_room = db.chat_room.Find(id);
            db.chat_room.Remove(chat_room);
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
