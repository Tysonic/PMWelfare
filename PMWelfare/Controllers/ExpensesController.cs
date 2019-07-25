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
    public class ExpensesController : Controller
    {
        private welfare db = new welfare();

        // GET: Expenses
        public ActionResult Index()
        {
            var expenses = db.Expenses.Include(e => e.SupProducts);
            return View(expenses.ToList());
        }

        // GET: Expenses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expense expense = db.Expenses.Find(id);
            if (expense == null)
            {
                return HttpNotFound();
            }
            return View(expense);
        }
       
        public ActionResult PreviousMonthExpense()
        {
          var expenses = from t1 in db.Expenses
                         join t2 in db.SupProducts on t1.ProductId equals t2.ProductId
                         join t3 in db.Celebrations on t2.EventId equals t3.EventId
                         join t4 in db.EventTypes on t3.EventTypeId equals t4.Id
                         where t3.EventDate.Value.Month == DateTime.Now.Month-1
                         select new Expense.MonthlyExpensesViewModel
                            {
                               Quantity = t1.Quantity,
                               ProductName = t2.ProductName,
                               EventName = t4.Type,
                               UnitPrice = t2.UnitPrice,
                               TotalPrice = t1.Quantity * t2.UnitPrice

                              };
            
            return View(expenses);
        }



        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(db.SupProducts, "ProductId", "ProductName");
            return View();
        }

        // POST: Expenses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ExpenseId,ExpenseDate,ProductId,Quantity,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt")] Expense expense)
        {
            expense.CreatedAt = DateTime.Now;
            expense.CreatedBy = "nicho";
            if (ModelState.IsValid)
            {
                db.Expenses.Add(expense);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductId = new SelectList(db.SupProducts, "ProductId", "ProductName", expense.ProductId);
            return View(expense);
        }

        // GET: Expenses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expense expense = db.Expenses.Find(id);
            if (expense == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(db.SupProducts, "ProductId", "ProductName", expense.ProductId);
            return View(expense);
        }

        // POST: Expenses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ExpenseId,ExpenseDate,ProductId,Quantity,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt")] Expense expense)
        {
            if (ModelState.IsValid)
            {
                db.Entry(expense).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(db.SupProducts, "ProductId", "ProductName", expense.ProductId);
            return View(expense);
        }

        // GET: Expenses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expense expense = db.Expenses.Find(id);
            if (expense == null)
            {
                return HttpNotFound();
            }
            return View(expense);
        }

        // POST: Expenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Expense expense = db.Expenses.Find(id);
            db.Expenses.Remove(expense);
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


        public ActionResult TotalExpense()
        {
            decimal? expense = db.Expenses.Join(db.SupProducts, s =>
            s.ProductId, e => e.ProductId, (s, e) =>
            new { u = e.UnitPrice, q = s.Quantity, s.ExpenseDate })
            .Where(s => s.ExpenseDate.Month == DateTime.Now.Month
             && s.ExpenseDate.Year == DateTime.Now.Year) 
             .Sum(s=> s.u*s.q).GetValueOrDefault()
             ;

            ViewBag.total = expense;

            return View();
        }
        public ActionResult CurrentExpenses()
        {
            var expenses = from t1 in db.Expenses
                           join t2 in db.SupProducts on t1.ProductId equals t2.ProductId
                           join t5 in db.Suppliers on t2.SupId equals t5.SupId
                           join t3 in db.Celebrations on t2.EventId equals t3.EventId
                           join t4 in db.EventTypes on t3.EventTypeId equals t4.Id
                           where t1.ExpenseDate.Month == DateTime.Now.Month
                           select new Expense.MonthlyExpensesViewModel
                           {
                               Quantity = t1.Quantity,
                               ProductName = t2.ProductName,
                               ExpenseDate = t1.ExpenseDate,
                               EventName = t3.EventName,
                               SupplierName = t5.SupName,
                               UnitPrice = t2.UnitPrice,
                               TotalPrice = t1.Quantity * t2.UnitPrice

                           };

            return View(expenses);
        }
        public ActionResult TotalPreviousMonthExpense()
        {
            var expenses = (from t1 in db.Expenses
                           join t2 in db.SupProducts on t1.ProductId equals t2.ProductId
                           join t3 in db.Celebrations on t2.EventId equals t3.EventId
                           where t3.EventDate.Value.Month == DateTime.Now.Month - 1
                           select (t1.Quantity * t2.UnitPrice)).Sum().GetValueOrDefault();

            ViewBag.Total = expenses;

            return View();
        }
    }
}
