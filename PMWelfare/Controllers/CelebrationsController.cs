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
    public class CelebrationsController : Controller
    {
        private welfare db = new welfare();

        // GET: Celebrations
        public ActionResult Index()
        {
            var celebrations = db.Celebrations.Include(c => c.EventType);
            return View(celebrations.ToList());
        }

        // GET: Celebrations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Celebration celebration = db.Celebrations.Find(id);
            if (celebration == null)
            {
                return HttpNotFound();
            }
            return View(celebration);
        }

        // GET: Celebrations/Create
        public ActionResult Create()
        {
            ViewBag.EventTypeId = new SelectList(db.EventTypes, "Id", "Type");
            return View();
        }

        // POST: Celebrations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EventId,EventName,EventDate,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,EventTypeId")] Celebration celebration)
        {
            if (ModelState.IsValid)
            {
                db.Celebrations.Add(celebration);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EventTypeId = new SelectList(db.EventTypes, "Id", "Type", celebration.EventTypeId);
            return View(celebration);
        }

        // GET: Celebrations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Celebration celebration = db.Celebrations.Find(id);
            if (celebration == null)
            {
                return HttpNotFound();
            }
            ViewBag.EventTypeId = new SelectList(db.EventTypes, "Id", "Type", celebration.EventTypeId);
            return View(celebration);
        }

        // POST: Celebrations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EventId,EventName,EventDate,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,EventTypeId")] Celebration celebration)
        {
            if (ModelState.IsValid)
            {
                db.Entry(celebration).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EventTypeId = new SelectList(db.EventTypes, "Id", "Type", celebration.EventTypeId);
            return View(celebration);
        }

        // GET: Celebrations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Celebration celebration = db.Celebrations.Find(id);
            if (celebration == null)
            {
                return HttpNotFound();
            }
            return View(celebration);
        }

        // POST: Celebrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Celebration celebration = db.Celebrations.Find(id);
            db.Celebrations.Remove(celebration);
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
            var events = db.Celebrations.Join(db.Celebrants, s =>
            s.EventId, m => m.EventId, (s, m) => new { s.EventType, s.EventDate, m.UserName, s.CreatedAt, s.Celebrants }).Where(s => 
            s.CreatedAt.Value.Month == DateTime.Now.Month
            && s.CreatedAt.Value.Year == DateTime.Now.Year)
            .Select(s => new { s.EventType, s.Celebrants,s.EventDate })
            .ToList();

            ViewBag.events = events;
            return View();
        }
        public ActionResult Totalevents()
        {
           var events = db.Celebrations.Where(s => s.CreatedAt
           .Value.Month == DateTime.Now.Month
            && s.CreatedAt.Value.Year == DateTime.Now.Year)
            .Select(s => s.EventId).Count();
            ViewBag.events = events;
            return View();
        }
    }
}
