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



    public class DepositsController : Controller
    {
        private welfare db = new welfare();

        public static void InsertFeedback(IEnumerable<Subscription> all)
        {

            // var sub = 
        }


        // GET: Deposits
        public ActionResult Index()
        {
            var deposits = db.Deposits.Include(d => d.Member);
            return View(deposits.ToList());
        }

        // GET: Deposits/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deposit deposit = db.Deposits.Find(id);
            if (deposit == null)
            {
                return HttpNotFound();
            }
            return View(deposit);
        }

        // GET: Deposits/Create
        public ActionResult Create()
        {
            ViewBag.UserName = new SelectList(db.Members, "UserName", "FirstName");
            return View();
        }



        // POST: Deposits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DepositID,UserName,Amount,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt")]
        Deposit deposit, Subscription subscriptions)
        {


            subscriptions.UserName = deposit.UserName;
            subscriptions.Amount = deposit.Amount;

           
                int month;
                int year;

                var users = db.Subscriptions.Select
                    (s => s.UserName).ToList();

                string user = subscriptions.UserName;

                if (users.Contains(user))
                {
                    var curr_year = db.Subscriptions.Where
                            (s => s.UserName == deposit.UserName)
                            .Select(s => s.SubYear).Max();

                    var cur_month = db.Subscriptions.Where
                        (s => s.UserName == deposit.UserName && s.SubYear == curr_year)
                        .Select(s => s.SubMonth).Max();
                    if (cur_month < 12)
                    {
                        year = db.Subscriptions.Where
                             (s => s.UserName == deposit.UserName)
                             .Select(s => s.SubYear).Max();
                        subscriptions.SubMonth = cur_month + 1;
                        subscriptions.SubYear = year;

                    }
                    else
                    {
                        year = db.Subscriptions.Where
                            (s => s.UserName == deposit.UserName)
                            .Select(s => s.SubYear).Max();

                        subscriptions.SubMonth = 1;
                        subscriptions.SubYear = year + 1;
                    };
                }
                else
                {

                    month = db.Members.Where(s => s.UserName == deposit.UserName)
                        .Select(s => s.CreatedAt.Value.Month).FirstOrDefault();
                    subscriptions.SubMonth = month;
                }











            if (ModelState.IsValid)
            {

                db.Subscriptions.Add(subscriptions);
                db.Deposits.Add(deposit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserName = new SelectList
                (db.Members, "UserName", "FirstName", deposit.UserName);
            return View(deposit);
        }

        // GET: Deposits/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deposit deposit = db.Deposits.Find(id);
            if (deposit == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserName = new SelectList(db.Members, "UserName", "FirstName", deposit.UserName);
            return View(deposit);
        }

        // POST: Deposits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DepositID,UserName,Amount,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt")] Deposit deposit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(deposit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserName = new SelectList(db.Members, "UserName", "FirstName", deposit.UserName);
            return View(deposit);
        }

        // GET: Deposits/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deposit deposit = db.Deposits.Find(id);
            if (deposit == null)
            {
                return HttpNotFound();
            }
            return View(deposit);
        }

        // POST: Deposits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Deposit deposit = db.Deposits.Find(id);
            db.Deposits.Remove(deposit);
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
        public ActionResult TotalCollection()
        {
            var Total = db.Deposits.Where(s => s.CreatedAt.Value.Month == DateTime.Now.Month
           && s.CreatedAt.Value.Year == DateTime.Now.Year).Sum(s => s.Amount).GetValueOrDefault();
            ViewBag.total = Total;
            return View();

        }
        public ActionResult CashAtHand()
        {
            var Total = db.Deposits.Where(s => s.CreatedAt.Value.Month == DateTime.Now.Month
            && s.CreatedAt.Value.Year == DateTime.Now.Year).Sum(s => s.Amount).GetValueOrDefault();

            var expense = db.Expenses.Join(db.SupProducts, s =>
            s.ProductId, e => e.ProductId, (s, e) =>
            new { u = e.UnitPrice, q = s.Quantity, s.ExpenseDate })
            .Where(s => s.ExpenseDate.Month == DateTime.Now.Month
             && s.ExpenseDate.Year == DateTime.Now.Year).Select(s => s.u * s.q).DefaultIfEmpty().Sum();

            var ClosingBalance = db.Monthlysummary.Where(s => s.CreatedAt.Value.Month == DateTime.Now.Month - 1
            && s.CreatedAt.Value.Year == DateTime.Now.Year).Select(s => s.ClosingBalance).FirstOrDefault();

            var Cash = Total + ClosingBalance - expense;
            ViewBag.total = Cash;
            return View();
        }
        //public ActionResult Subscribe()
        //{
        //    var sub = db.Deposits.Select(s => new { s.UserName }).ToList();


        //    List<string> subs = sub.Except(db.Subscriptions.Select(s => s.UserName).ToList());
        //    List<Deposit> ss = new List<Deposit>();
        //    subs.ForEach(s => ss.Add(new Deposit(s)));


        //    return View();
        //}
    }
}
