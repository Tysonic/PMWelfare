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
    public class celebrationsController : Controller
    {
        private welfare db = new welfare();

        // GET: celebrations
        public ActionResult Index()
        {
            return View(db.celebrations.ToList());
        }


        public ActionResult TotlaEvents()
        {
            var eventsum = db.celebrations.Where(s => s.event_date.Month == DateTime.Now.Month && s.event_date.Year == DateTime.Now.Year).Select(s=>s.event_name).Count();
                ViewBag.total = eventsum;

            var eventnames = db.celebrations.GroupBy(s => s.event_name).Count();
            ViewBag.events = eventnames;
            
            
            return View();
        }
        
        // GET: celebrations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            celebration celebration = db.celebrations.Find(id);
            if (celebration == null)
            {
                return HttpNotFound();
            }
            return View(celebration);
        }

        // GET: celebrations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: celebrations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "event_id,event_name,event_date,created_by,created_at,updated_by,updated_at")] celebration celebration)
        {
            if (ModelState.IsValid)
            {
                db.celebrations.Add(celebration);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(celebration);
        }

        // GET: celebrations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            celebration celebration = db.celebrations.Find(id);
            if (celebration == null)
            {
                return HttpNotFound();
            }
            return View(celebration);
        }

        // POST: celebrations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "event_id,event_name,event_date,created_by,created_at,updated_by,updated_at")] celebration celebration)
        {
            if (ModelState.IsValid)
            {
                db.Entry(celebration).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(celebration);
        }

        // GET: celebrations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            celebration celebration = db.celebrations.Find(id);
            if (celebration == null)
            {
                return HttpNotFound();
            }
            return View(celebration);
        }

        // POST: celebrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            celebration celebration = db.celebrations.Find(id);
            db.celebrations.Remove(celebration);
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
