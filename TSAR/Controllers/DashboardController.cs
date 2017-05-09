using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TSAR.Controllers
{
    public class DashboardController : Controller
    {
        [Authorize(Roles = "Admin")]
        // GET: Dashboard
        public ActionResult Dashboard()
        {
            return View();
        }
    }
}