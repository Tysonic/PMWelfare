using PMWelfare.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

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
        public ActionResult Create([Bind(Include = "DepositID," +
            "UserName,Amount,UpdatedBy,UpdatedAt")]
        Deposit deposit, Subscription subscriptions )
        {



            deposit.CreatedAt = DateTime.Now;
            deposit.CreatedBy = "nicho";
                decimal? SubAmount = 20000;
                var users = db.Subscriptions.Select
                        (s => s.UserName).ToList();
                decimal? amount = deposit.Amount;


            int year;
            int month;

            if (amount > 0 )
            {
                if (amount <= SubAmount)
                {
                    if (users.Contains(deposit.UserName))
                    {
////////////////////////////////////
/////////////variables /////////////
////////////////////////////////////
                        subscriptions.UserName = deposit.UserName;
                        year = db.Subscriptions.Where
                          (s => s.UserName == deposit.UserName)
                           .Select(s => s.SubYear).Max();

                        month = db.Subscriptions.Where
                        (s => s.UserName == deposit.UserName && s.SubYear == year).DefaultIfEmpty()
                        .Select(s => s.SubMonth).Max();

                        decimal? AmountSubscribed = db.Subscriptions.Where
                        (s => s.UserName == deposit.UserName && s.SubYear == year
                        && s.SubMonth == month).Select(m => m.Amount).Single();
////////////////////////////////////
/////////////variables /////////////
////////////////////////////////////

                        if (AmountSubscribed < SubAmount)
                        {
//////////////////////////////////
//////////// partial arrears//////
//////////////////////////////////

                            decimal? balance = SubAmount - AmountSubscribed;

                            subscriptions = db.Subscriptions.Where(s => s.SubMonth == month &&
                             s.SubYear == year && s.UserName == deposit.UserName).Select(l => l).Single();
                            db.Subscriptions.Find(subscriptions.SubId);
                            if (balance >= amount && amount > 0)
                            {
                                subscriptions.Amount = subscriptions.Amount + amount;

                                db.Entry(subscriptions).State = EntityState.Modified;
                                db.SaveChanges();
                            }
                            else
                            {
                                subscriptions.Amount = subscriptions.Amount + balance;
                                db.Entry(subscriptions).State = EntityState.Modified;
                                db.SaveChanges();
                                amount -= balance;
                                if (month < 12) { subscriptions.SubMonth = ++month; subscriptions.SubYear = year; }
                                else { subscriptions.SubYear = ++year; subscriptions.SubMonth = 1; }
                                subscriptions.Amount = amount;
                                db.Subscriptions.Add(subscriptions);
                                db.SaveChanges();
                            }
                            
                        }

                        
                        else
                        {

                            subscriptions.Amount = deposit.Amount;

/////////////////////////////////////////
//users who have ever subscribbed ///////
//whose current deposit is less /////////
//than or equal to subscription amount///
//with no partial arrears////////////////
/////////////////////////////////////////
                            if (month < 12)
                            {

                                subscriptions.SubMonth = ++month;
                                subscriptions.SubYear = year;
                                db.Subscriptions.Add(subscriptions);

                            }
                            else
                            {
                                subscriptions.SubMonth = 1;
                                subscriptions.SubYear = ++year;
                                db.Subscriptions.Add(subscriptions);
                            }
                        }
                    }

                    else
                    {
///////////////////////////////////////
//users who haven't subscribed/////////
//whose current deposit is less than///
//or equal to the amount to subscribe//
///////////////////////////////////////

                        month = db.Members.Where(s => deposit.UserName == s.UserName)
                           .Select(s => s.CreatedAt).Single().Value.Month;
                        year = db.Members.Where(s => deposit.UserName == s.UserName)
                       .Select(s => s.CreatedAt).Single().Value.Year;

                        subscriptions.SubMonth = month;
                        subscriptions.SubYear = year;
                        subscriptions.Amount = amount;
                        db.Subscriptions.Add(subscriptions);


                    }        
                }
                else
                {
//////////////////////////////
//current deposit is greater// 
//than subscription amount////
//////////////////////////////

                    if (users.Contains(deposit.UserName))
                    {

////////////////////////////////////
/////////////variables /////////////
////////////////////////////////////
                        subscriptions.UserName = deposit.UserName;
                        year = db.Subscriptions.Where
                          (s => s.UserName == deposit.UserName)
                           .Select(s => s.SubYear).Max();

                        month = db.Subscriptions.Where
                        (s => s.UserName == deposit.UserName && s.SubYear == year)
                        .Select(s => s.SubMonth).Max();

                        decimal? AmountSubscribed = db.Subscriptions.Where
                        (s => s.UserName == deposit.UserName && s.SubYear == year
                        && s.SubMonth == month).Select(m => m.Amount).Single();
////////////////////////////////////
/////////////variables /////////////
////////////////////////////////////



 //////////////////////////////
//member has ever subscribed//
//////////////////////////////

                        decimal? balance = SubAmount - AmountSubscribed;

                        subscriptions = db.Subscriptions.Where(s => s.SubMonth == month &&
                         s.SubYear == year && s.UserName == deposit.UserName).Select(l => l).Single();
                        db.Subscriptions.Find(subscriptions.SubId);

                        if (AmountSubscribed < SubAmount)
                        {
                            subscriptions.Amount = subscriptions.Amount + balance;
                            db.Entry(subscriptions).State = EntityState.Modified;
                            db.SaveChanges();
                            amount -= balance;
                        }


                        for (decimal? x = amount; x > 0; x -= SubAmount)
                        {
///////////////////////////////////////////////////////////////
//checking for changes in month and year///////////////////////
///////////////////////////////////////////////////////////////
                            if (month < 12)
                            {
                                subscriptions.SubMonth = ++month;
                                subscriptions.SubYear = year;
                            }

                            else
                            {
                                month = 0;
                                ++month;
                                subscriptions.SubMonth = month;
                                subscriptions.SubYear = ++year;
                            }
//////////////////////////////////////////////////////////////////
                            if (x >= SubAmount)
                            {
                                Subscription subs = new Subscription
                                {
                                    Amount = SubAmount
                                };
                                subscriptions.Amount = subs.Amount;
                                db.Subscriptions.Add(subscriptions);
                                if (ModelState.IsValid)
                                {
                                    db.SaveChanges();
                                }
                            }
                            if (x < SubAmount)
                            {
                                subscriptions.Amount = x;
                                db.Subscriptions.Add(subscriptions);
                                db.SaveChanges();
                            }
                        }
                    }
                    else
                    {

////////////////////////////////////////////////////////////////////////////////////////////////////////
//members who haven't subscribed and current deposit is more than subscriptions amount//////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////


                        month = db.Members.Where(s => deposit.UserName == s.UserName)
                           .Select(s => s.CreatedAt).Single().Value.Month;
                        year = db.Members.Where(s => deposit.UserName == s.UserName)
                       .Select(s => s.CreatedAt).Single().Value.Year;


                        for (decimal? x = amount; x >= 0; x -= SubAmount)
                        {
                            if (month < 12)
                            {
                                subscriptions.SubMonth = ++month;
                                subscriptions.SubYear = year;
                            }
                            else
                            {
                                month = 0;
                                ++month;
                                subscriptions.SubMonth = month;
                                subscriptions.SubYear = ++year;
                            }
                            {
                                if (x >= SubAmount )
                                {
                                    Subscription subs = new Subscription
                                    {
                                        Amount = SubAmount
                                    };
                                    subscriptions.Amount = subs.Amount;
                                    db.Subscriptions.Add(subscriptions);
                                    if (ModelState.IsValid)
                                    {
                                        db.SaveChanges();
                                    }
                                }
                                if (x < SubAmount && x<0)
                                {
                                    subscriptions.Amount = x;
                                    db.Subscriptions.Add(subscriptions);
                                    db.SaveChanges();
                                }
                            }

                        }

                    }
                }
            }




            if (ModelState.IsValid )
            {


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
            decimal? Total = db.Deposits.Where(s => s.CreatedAt
            .Value.Month == DateTime.Now.Month
           && s.CreatedAt.Value.Year == DateTime.Now.Year)
           .Sum(s =>s.Amount) ?? 0;
            ViewBag.total = Total;
            return View();

        }
        public ActionResult CashAtHand()
        {
            var Total = db.Deposits.Where(s => 
            s.CreatedAt.Value.Month == DateTime.Now.Month
            && s.CreatedAt.Value.Year == DateTime.Now.Year)
            .DefaultIfEmpty().Sum(s => s.Amount) ??0;

            var  expense = db.Expenses.Join(db.SupProducts, s =>
            s.ProductId, e => e.ProductId, (s, e) =>
            new { u = e.UnitPrice, q = s.Quantity, s.ExpenseDate })
            .Where(s => s.ExpenseDate.Month == DateTime.Now.Month
             && s.ExpenseDate.Year == DateTime.Now.Year).DefaultIfEmpty()
             .Sum(s => s.u * s.q) ?? 0;

            decimal? ClosingBalance = db.Monthlysummary
                .Where(s => s.EndDate.Value.Month == DateTime.Now.Month - 1
            && s.EndDate.Value.Year == DateTime.Now.Year)
            .Select(s => s.ClosingBalance).FirstOrDefault();

           decimal? Cash = Total + ClosingBalance - expense;
            ViewBag.total = Cash.GetValueOrDefault();
            return View();
        }

    }
}

