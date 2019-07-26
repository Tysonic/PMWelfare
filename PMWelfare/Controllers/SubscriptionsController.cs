using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using PMWelfare.Models;
using static PMWelfare.Models.Subscription;

namespace PMWelfare.Controllers
{
    public class SubscriptionsController : Controller
    {
        private welfare db = new welfare();

        // GET: Subscriptions
        public ActionResult Index()
        {
            ViewBag.submonth = db.Subscriptions.Select(s => s.SubMonth).DefaultIfEmpty().Distinct();
            ViewBag.subyear = db.Subscriptions.Select(x => x.SubYear).DefaultIfEmpty().Distinct();

            var subscriptions = db.Subscriptions.Include(s => s.Member).ToList();
            return View(subscriptions);
        }
        [HttpPost]
        public ActionResult Index(int submonth, int subyear)
        {
            ViewBag.submonth = db.Subscriptions.Select(s => s.SubMonth).DefaultIfEmpty().Distinct();
            ViewBag.subyear = db.Subscriptions.Select(x => x.SubYear).DefaultIfEmpty().Distinct();


            var subscriptions = db.Subscriptions.Where(s => s.SubMonth ==
            submonth && s.SubYear == subyear).Include(s => s.Member).ToList();
            Session["students"] = subscriptions.ToList<Subscription>();
            return View(subscriptions.ToList());
        }
        // GET: ExportData
        public void ExportToExcel()
        {
            var data1 = (List<Subscription>)Session["students"];
            // Step 1 - get the data from database
            var data = db.Subscriptions.Include(s => s.Member).ToList();

            // instantiate the GridView control from System.Web.UI.WebControls namespace
            // set the data source
            GridView gridview = new GridView
            {
                DataSource = data1
            };
            gridview.DataBind();

            // Clear all the content from the current response
            Response.ClearContent();
            Response.Buffer = true;
            // set the header
            Response.AddHeader("content-disposition", "attachment;filename = subscriptions.xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            // create HtmlTextWriter object with StringWriter
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    // render the GridView to the HtmlTextWriter
                    gridview.RenderControl(htw);
                    // Output the GridView content saved into StringWriter
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
            
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
        
        public ActionResult Advances()
        {
            decimal? advance = db.Subscriptions.Where(s => 
            (s.SubMonth > DateTime.Now.Month && s.SubYear == DateTime.Now.Year)
           || s.SubYear > DateTime.Now.Year).Select(s => s.Amount).DefaultIfEmpty().Sum()??0;

            ViewBag.Advances = advance;

            return View();
        }

        public ActionResult Arrears() {
  
             int? month = DateTime.Now.Month;
                int? year = DateTime.Now.Year;

                var subscribers = db.Subscriptions
                   .Where(s => s.SubMonth == month && s.SubYear == year)
                   .Select(s => s.UserName).Distinct().ToList();
                var all = db.Members.Select(s => s.UserName)
                .Distinct().ToList();
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
                    int? maxyear = db.Subscriptions
                        .Where(s => s.UserName == m.UserName )
                        .Select(s => s.SubYear).Max();
                    int? yeardif = year - maxyear;

                    int? maxmonth = db.Subscriptions
                        .Where(s => s.UserName == m.UserName && s.SubYear == maxyear)
                        .Select(s => s.SubMonth).Max();

                    if (maxmonth < month && maxyear<=year)
                    {
                        int? months = DateTime.Now.Month - maxmonth;
                        decimal? arrearAmount = months * 20000;
                        arrears.Add(new Subscription(m.UserName, arrearAmount));
                        //ViewBag.k = arrearAmount;
                    }

                }
                

            }
          
           //new member who have not subscribed yet
            var ful = all.Except(jr).ToList();
            ful.ForEach(h => newmemb.Add(new Subscription(h)));
            foreach (var mmm in newmemb)
            {

                int max = db.Members.
                    Where(d => d.UserName == mmm.UserName)
                    .Select(d => d.CreatedAt.Value.Month).Single();
                int yea = db.Members.Where(d => d.UserName ==
                mmm.UserName)
                           .Select(d => d.CreatedAt.Value.Year).Single();
                    int? ju = year - yea;
                    int? month1 = month - max + 12 * ju;
                    if (month1 > 0)
                    {
                        int? arrearAm = month1 * 20000;
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

            return View(arrears);

        }
         public ActionResult Advancelist()
        {
            var advances = db.Subscriptions.Where(s => (s.SubMonth
            > DateTime.Now.Month && s.SubYear == DateTime.Now.Year)
             || s.SubYear > DateTime.Now.Year).
             Select(s => new AdvancesViewModel{Amount=s.Amount, UserName=s.UserName }).ToList();
            return View(advances);
        }

        public ActionResult TotalArrears()
        {

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
                    int? maxyear = db.Subscriptions
                        .Where(s => s.UserName == m.UserName)
                        .Select(s => s.SubYear).Max();
                    int? yeardif = year - maxyear;

                    int? maxmonth = db.Subscriptions
                        .Where(s => s.UserName == m.UserName && s.SubYear == maxyear)
                        .Select(s => s.SubMonth).Max();

                    if (maxmonth < month && maxyear <= year)
                    {
                        int? months = DateTime.Now.Month - maxmonth;
                        decimal? arrearAmount = months * 20000;
                        arrears.Add(new Subscription(m.UserName, arrearAmount));
                        //ViewBag.k = arrearAmount;
                    }

                }


            }

            //new member who have not subscribed yet
            var ful = all.Except(jr).ToList();
            ful.ForEach(h => newmemb.Add(new Subscription(h)));
            foreach (var mmm in newmemb)
            {

                int? max = db.Members.
                    Where(d => mmm.UserName==d.UserName)
                    .Select(d => d.CreatedAt.Value.Month).Single();
                int? yea = db.Members.Where(d => d.UserName == mmm.UserName)
                           .Select(d => d.CreatedAt.Value.Year).Single();
                int? ju = year - yea;
                int? month1 = month - max + 12 * ju;
                if (month1 > 0)
                {
                    int? arrearAm = month1 * 20000;
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

            return View(arrears);

        }

        [ChildActionOnly]
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
       
    }

    internal class Subs
    {
    }
}
