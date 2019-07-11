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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult Advances()
        {
            var advances = db.Subscriptions.Where(s => (s.SubMonth
            > DateTime.Now.Month && s.SubYear == DateTime.Now.Year)
             || s.SubYear> DateTime.Now.Year).DefaultIfEmpty().Sum(a => a.Amount);
            ViewBag.advance = advances;
            return View();
        }

        int month = DateTime.Now.Month;
        int year =DateTime.Now.Year;

        //public string Arrears()
        //{
        //    var subscribers = db.Subscriptions.Where(s => s.SubMonth == month
        //    && s.SubYear == year).Select(s => s.UserName).Distinct().ToList();

        //    var all = db.Members.Select(s => s.UserName).ToList();
        //    List<string> notSubscribed_ = all.Except(subscribers).ToList();

        //    List<Subscription> dz = new List<Subscription>();
        //    notSubscribed_.ForEach(s => dz.Add(new Subscription(s)));

        //    return dz.ToString();

        //}

        //public ActionResult areas()
        //{
            //    int loop = Arrears().Count();

            //    do
            //    {
            //        var arr = Arrears();

            //        //if (month > 1)
            //        //{
            //        //    month--;
            //        //}
            //        //else
            //        //{
            //            month = 12;
            //            year--;
            //        //}
            //        //arr += arr;

            //        //List<string>arrr = arr.ToString().ToList();


            //        //List<Subscription> dz = new List<Subscription>();
            //        //arrr.ForEach(s => dz.Add(new Subscription(s)));
            //        return View(arr);
            //    } while (loop != 0);
            //}

            public ActionResult areas()
            {
                var subscribers = db.Subscriptions.Where(s => s.SubMonth == month
                && s.SubYear == year).Select(s => s.UserName).Distinct().ToList();

                var all = db.Members.Select(s => s.UserName).ToList();
                List<string> notSubscribed_ = all.Except(subscribers).ToList();

                List<Subscription> dz = new List<Subscription>();
                notSubscribed_.ForEach(s => dz.Add(new Subscription(s)));
                return View(dz);

            }
        }
    }

