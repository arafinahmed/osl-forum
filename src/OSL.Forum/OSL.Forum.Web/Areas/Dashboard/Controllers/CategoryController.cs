using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OSL.Forum.Web.Areas.Dashboard.Controllers
{
    [Authorize(Roles ="SuperAdmin")]
    public class CategoryController : Controller
    {
        // GET: Dashboard/Category
        public ActionResult Index()
        {
            return View();
        }
    }
}