using PMWelfare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMWelfare.Controllers
{
    public class ReportsController : Controller
    {
        private welfare db = new welfare();
        // GET: QueryClass
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Total()
        {
            var tot = db.deposits.Where(s => s.created_at.Value.Month == DateTime.Now.Month && s.created_at.Value.Year == DateTime.Now.Year).Sum(x => x.amount).ToString();

            ViewBag.Total = tot;

            return View();
        }

    }
}