using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ninesky.Web.Areas.Member.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        //
        // GET: /Member/Home/
        public ActionResult Index()
        {
            return View();
        }
	}
}