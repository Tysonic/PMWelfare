using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PMWelfare.Models;
using static PMWelfare.Models.Celebration;

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
            ViewBag.EventTypeId = new SelectList(db.EventTypes, 
                "Id", "Type");
            return View();
        }

        // POST: Celebrations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = 
            "EventId,EventDate,CreatedBy,CreatedAt,UpdatedBy," +
            "UpdatedAt,EventTypeId,EventName")] Celebration celebration)
        {
            celebration.CreatedAt = DateTime.Now;
            celebration.CreatedBy = "nicho";
            if (ModelState.IsValid)
            {
                db.Celebrations.Add(celebration);
                db.SaveChanges();
                return RedirectToAction("Create","Celebrants", 
                    new {area= "" });
            }

            ViewBag.EventTypeId = new SelectList(db.EventTypes, 
                "Id", "Type", celebration.EventTypeId);
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
        public ActionResult Edit([Bind(Include = "EventName,EventId,EventDate,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,EventTypeId")] Celebration celebration)
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

        public ActionResult UpComingEvents()
        {

            var events = db.Celebrants.Join(db.Celebrations, m =>
            m.EventId, s => s.EventId, (m, s) => new Celebrationsviewmodel
            {
                EventType = s.EventType.Type,
                EventDate = s.EventDate,
                UserName = m.UserName
            }).Where(s => s.EventDate.Month == (DateTime.Now.Month))
            .DefaultIfEmpty().ToList();
            return View(events);
        }

        public ActionResult TotalUpComingEvents()
        {

            var events = db.Celebrants.Join(db.Celebrations, m =>
            m.EventId, s => s.EventId, (m,s)=> new { s.EventDate,s.EventId})
            .Where(s =>s.EventDate.Month==DateTime.Now.Month)
            .Select(s=>s.EventId).Count();
            @ViewBag.total = events;
            return View();
        }

        public ActionResult EventsOfProviousMonth()
        {

            var events = db.Celebrants.Join(db.Celebrations, m =>
            m.EventId, s => s.EventId, (m, s) => new Celebrationsviewmodel
            {
                EventType = s.EventType.Type,
                EventDate = s.EventDate,
                UserName = m.UserName
            }).Where(s => DbFunctions.DiffMonths(s.EventDate,DateTime.Now)==1)
            .ToList();
            return View(events);
        }




        public ActionResult TotaleventsOfPreviousMonth()
        {
           int events = db.Celebrations.Where
                (s =>s.EventDate.Month==(DateTime.Now.Month-1))
            .Select(s => s.EventId).Count();
            ViewBag.events = events;
            return View(ViewBag.events);
        }
    }
}
