﻿using System;
using System.Collections.Generic;
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
            var ClosingBalance = db.Monthlysummary.Where(s => s.EndDate.Month == DateTime.Now.Month
            && s.EndDate.Year == DateTime.Now.Year).Select(s => s.ClosingBalance).FirstOrDefault();
            ViewBag.balance = ClosingBalance;
            return View();
        }
    }
}