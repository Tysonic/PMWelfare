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
        public ActionResult Create([Bind(Include = "SubId,UserName,Amount,SubMonth,SubYear")] Subscription subscription)
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
        public ActionResult Edit([Bind(Include = "SubId,UserName,Amount,SubMonth,SubYear")] Subscription subscription)
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

             decimal? amount = advances.Sum(a => a.Amount);
            ViewBag.advance = advances;
            return View(ViewBag.advance);
        }

        public ActionResult Advancelist()
        {
            var advances = db.Subscriptions.Where(s => (s.SubMonth
            > DateTime.Now.Month && s.SubYear == DateTime.Now.Year)
             || s.SubYear > DateTime.Now.Year).
             Select(s => new { s.Amount, s.UserName }).ToList();
            return View(advances);
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
