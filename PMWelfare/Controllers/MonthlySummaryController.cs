using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PMWelfare.Models;

namespace PMWelfare.Controllers

{
    public class MonthlySummaryController : Controller
    {
        private welfare db = new welfare();
        // GET: MonthlySummary

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Closing()
        {
            var Balance = db.Monthlysummary
                .Where(s => DbFunctions.DiffDays(DateTime.Now, s.EndDate)
            <= 10 && DbFunctions.DiffDays(DateTime.Now, s.EndDate) ==1)
            .Select(s => s.ClosingBalance).FirstOrDefault();

            ViewBag.balance = Balance;
            return View();
        }
    }
}