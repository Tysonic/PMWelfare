using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
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

            return PartialView(advance);
        }
        public ActionResult Arrears() {
            
           

             int month = DateTime.Now.Month;
                int year = DateTime.Now.Year;

                var subscribers = db.Subscriptions
                   .Where(s => s.SubMonth == month && s.SubYear == year)
                   .Select(s => s.UserName).Distinct().ToList();
                var all = db.Members.Select(s => s.UserName).Distinct().ToList();
                List<string> notsub = all.Except(subscribers).ToList();
                List<Subscription> user = new List<Subscription>();
            List<Subscription> arrears = new List<Subscription>();
            List<Subscription> subs = new List<Subscription>();
            List<Subscription> newmemb = new List<Subscription>();
            notsub.ForEach(s => user.Add(new Subscription(s)));
            var jr = db.Subscriptions.Select(s => s.UserName);
       
            foreach (var m in user)
            {
                var jrm = db.Subscriptions.Select(s => s.UserName);
                //ViewBag.u = user;
                if (jrm.Contains(m.UserName))
                {
                    int maxmonth = db.Subscriptions
                        .Where(s => s.UserName == m.UserName).Select(s => s.SubMonth).Max();
                    int months = DateTime.Now.Month - maxmonth;
                    int arrearAmount = months * 20000;
                    arrears.Add(new Subscription(m.UserName, arrearAmount));
                    //ViewBag.k = arrearAmount;


                }
               

            }
          
            //new member who have not subscribed yet
            var ful = all.Except(jr);
            ful.ForEach(h => newmemb.Add(new Subscription(h)));
            foreach (var mmm in newmemb)
            {

                int max = db.Members.
                    Where(d => d.UserName == mmm.UserName)
                    .Select(d => d.CreatedAt.Value.Month).Max();
                int yea = db.Members.Where(d => d.UserName == mmm.UserName)
                           .Select(d => d.CreatedAt.Value.Year).Max();
                if (yea == DateTime.Now.Year)
                {
                    int month1 = DateTime.Now.Month - max;
                    int arrearAm = month1 * 20000;
                    arrears.Add(new Subscription(mmm.UserName, arrearAm));
                }
                if (DateTime.Now.Year > yea)
                {
                    int ju = DateTime.Now.Year - yea;
                    int month1 = DateTime.Now.Month - max + 12 * ju;
                    int arrearAm = month1 * 20000;
                    arrears.Add(new Subscription(mmm.UserName, arrearAm));

                }



            }
            //get the partial arrears of a subscriber
            subscribers.ForEach(hm => subs.Add(new Subscription(hm)));
            foreach (var jt in subs)
            {
                decimal? amount = db.Subscriptions
                        .Where(s => s.UserName == jt.UserName && s.SubMonth == month && s.SubYear == year)
                        .Select(s => s.Amount).Single();
                decimal? subamount = 20000;
                if (amount < subamount)
                {
                    decimal? subamount1 = subamount - amount;

                    arrears.Add(new Subscription(jt.UserName, subamount1));
                }

            }



            ViewBag.are = arrears.Sum(x => x.Amount);


            
            //for (int i = 5; i > 0; i--)
            //{
            //    iterationDate = iterationDate - 1;
            //    subscribers = subscribers.Union(
            //        db.Subscriptions.Where(
            //        s => s.SubMonth == iterationDate
            //            && s.SubYear == year).AsEnumerable()
            //    );

            //}
            //var subscriberNames = subscribers.Select(s =>new { s.UserName,s.SubMonth }).ToList();
            //foreach(var u   in subscriberNames){

            //    subsiber.Add(new Subscription(u.UserName,u.SubMonth) );
            //}
            // var sub = db.Members.Select(m => m.UserName)
            //.Where(u => subscriberNames.All(s => s.UserName != u)).ToList();
            //foreach(string sx in subscriberNames)
            //{
            //    user.Add(new Subscription(sx));
            //    ViewBag.k = sx.Distinct().Count();



            //}
            // sub.ForEach(s => user.Count().Add(new Subscription(s)));


            return View(arrears);
 

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
