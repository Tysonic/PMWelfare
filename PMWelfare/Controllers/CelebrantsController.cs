using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PMWelfare.Models;
using static PMWelfare.Models.Celebrant;

namespace PMWelfare.Controllers
{
    public class CelebrantsController : Controller
    {
        private welfare db = new welfare();

        // GET: Celebrants
        public ActionResult Index()
        {
            var celebrants = db.Celebrants.Include(c => c.Celebration).Include(c => c.Member);
            return View(celebrants.ToList());
        }

        // GET: Celebrants/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Celebrant celebrant = db.Celebrants.Find(id);
            if (celebrant == null)
            {
                return HttpNotFound();
            }
            return View(celebrant);
        }

        // GET: Celebrants/Create
        public ActionResult Create()
        {
            ViewBag.EventId = new SelectList(db.Celebrations, "EventId", "EventName");
            ViewBag.UserName = new SelectList(db.Members, "UserName", "FirstName");
            return View();
        }

        // POST: Celebrants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CelebId,UserName,EventId")] Celebrant celebrant)
        {
            if (ModelState.IsValid)
            {
                db.Celebrants.Add(celebrant);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EventId = new SelectList(db.Celebrations, "EventId", "EventName", celebrant.EventId);
            ViewBag.UserName = new SelectList(db.Members, "UserName", "FirstName", celebrant.UserName);
            return View(celebrant);
        }

        // GET: Celebrants/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Celebrant celebrant = db.Celebrants.Find(id);
            if (celebrant == null)
            {
                return HttpNotFound();
            }
            ViewBag.EventId = new SelectList(db.Celebrations, "EventId", "EventName", celebrant.EventId);
            ViewBag.UserName = new SelectList(db.Members, "UserName", "FirstName", celebrant.UserName);
            return View(celebrant);
        }

        // POST: Celebrants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CelebId,UserName,EventId")] Celebrant celebrant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(celebrant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EventId = new SelectList(db.Celebrations, "EventId", "EventName", celebrant.EventId);
            ViewBag.UserName = new SelectList(db.Members, "UserName", "FirstName", celebrant.UserName);
            return View(celebrant);
        }

        // GET: Celebrants/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Celebrant celebrant = db.Celebrants.Find(id);
            if (celebrant == null)
            {
                return HttpNotFound();
            }
            return View(celebrant);
        }

        // POST: Celebrants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Celebrant celebrant = db.Celebrants.Find(id);
            db.Celebrants.Remove(celebrant);
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
        public ActionResult Events()
        {

            var events = db.Celebrants.Join(db.Celebrations, m =>
            m.EventId, s => s.EventId, (m, s) => new Celebransviewmodel
            {
                EventType = s.EventType.Type,
                EventDate = s.EventDate,
                UserName = m.UserName
            });
            //    .Where(s =>
            //DbFunctions.DiffDays(DateTime.Now , s.EventDate) <=10 ).DefaultIfEmpty().ToList();

            ViewBag.events = events;
            return View(events);
        }
    }
}
