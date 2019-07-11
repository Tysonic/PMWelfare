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
    public class SubscriptionsController : Controller
    {
        private welfare db = new welfare();

        // GET: Subscriptions
        public ActionResult Index()
        {
            var subscriptions = db.Subscriptions.Include(s => s.Member);
            return View(subscriptions.ToList());
        }

        // GET: Subscriptions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subscription subscription = db.Subscriptions.Find(id);
            if (subscription == null)
            {
                return HttpNotFound();
            }
            return View(subscription);
        }

        // GET: Subscriptions/Create
        public ActionResult Create()
        {
            ViewBag.UserName = new SelectList(db.Members, "UserName", "FirstName");
            return View();
        }

        // POST: Subscriptions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SubId,UserName,Amount,SubMonth,SubYear,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt")] Subscription subscription)
        {
            if (ModelState.IsValid)
            {
                db.Subscriptions.Add(subscription);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserName = new SelectList(db.Members, "UserName", "FirstName", subscription.UserName);
            return View(subscription);
        }

        // GET: Subscriptions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subscription subscription = db.Subscriptions.Find(id);
            if (subscription == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserName = new SelectList(db.Members, "UserName", "FirstName", subscription.UserName);
            return View(subscription);
        }

        // POST: Subscriptions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SubId,UserName,Amount,SubMonth,SubYear,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt")] Subscription subscription)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subscription).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserName = new SelectList(db.Members, "UserName", "FirstName", subscription.UserName);
            return View(subscription);
        }

        // GET: Subscriptions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subscription subscription = db.Subscriptions.Find(id);
            if (subscription == null)
            {
                return HttpNotFound();
            }
            return View(subscription);
        }

        // POST: Subscriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Subscription subscription = db.Subscriptions.Find(id);
            db.Subscriptions.Remove(subscription);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [ChildActionOnly]
        public ActionResult CurrentAdvances()
        {
            var ad = (from t1 in db.Subscriptions
                      where ((t1.SubMonth > DateTime.Now.Month && t1.SubYear == DateTime.Now.Year)
                      || t1.SubYear > DateTime.Now.Year)
                      select t1.Amount).DefaultIfEmpty().Sum();

            var advance = db.Subscriptions.Where(s => (s.SubMonth > DateTime.Now.Month && s.SubYear == DateTime.Now.Year)
           || s.SubYear > DateTime.Now.Year).DefaultIfEmpty().Sum(s => s.Amount);

            ViewBag.Advances = advance;

            return PartialView();
        }
        public ActionResult Arrears() {
            //var today = DateTime.Now;
            //int month = today.Month;
            //int year = today.Year;
            //int monthFour = today.AddMonths(-4).Month;//this gets the last 4th month
            //List<string> subscriber = db.Subscriptions
            //              .Where(s => s.SubMonth <= month && s.SubMonth >= monthFour && s.SubYear == year)
            //              .Select(s => s.UserName).ToList();
            //var all = db.Members.Select(s => s.UserName).ToList();
            //List<Subscription> play = new List<Subscription>();
            //List<Subscription> user = new List<Subscription>();
            //subscriber.ForEach(s => play.Add(new Subscription(s)));
            //List<string> notsub = all.Except(subscriber).ToList();
            //notsub.ForEach(s => user.Add(new Subscription(s)));

            //{ int month = DateTime.Now.Month;
            //    int year = DateTime.Now.Year;
            //if () { }
            //var subscribers = db.Subscriptions
            //   .Where(s => s.SubMonth == month && s.SubYear == year)
            //   .Select(s => s.UserName).Distinct().ToList();
            //var all = db.Members.Select(s => s.UserName).Distinct().ToList();

            //List<string> notsub = all.Except(subscribers).ToList();

            //List<Subscription> user = new List<Subscription>();
            //List<Subscription> queries = new List<Subscription>();
            //notsub.ForEach(s => user.Add(new Subscription(s)));

            List<Subscription> user = new List<Subscription>();
            DateTime iterationDate = DateTime.UtcNow;
            var subscribers =
                db.Subscriptions.Where(
                    s => s.SubMonth == iterationDate.Month
                        && s.SubYear == iterationDate.Year);

            for (int i = 5; i != 0; i--)
            {
                iterationDate = iterationDate.AddMonths(-1);
                subscribers = subscribers.Union(
                    db.Subscriptions.Where(
                    s => s.SubMonth == iterationDate.Month
                        && s.SubYear == iterationDate.Year).ToList()
                );

            }
            var subscriberNames = subscribers.Select(s => s.UserName).ToList();
            var sub = db.Members.Select(m => m.UserName)
           .Where(u => subscriberNames.All(s => s != u))
           .ToList();
            sub.ForEach(s => user.Add(new Subscription(s)));
              foreach(var m in user)
            {
                var u = m.UserName.ToString();
                

            }

            return View(user);
 

        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        //private  string  GetArrears()

        //{
            
            
        //}

    }
}
