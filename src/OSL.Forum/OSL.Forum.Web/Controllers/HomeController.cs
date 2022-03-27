using Autofac;
using log4net;
using OSL.Forum.Web.Models.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OSL.Forum.Web.Controllers
{
    public class HomeController : Controller
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(HomeController));
        private readonly ILifetimeScope _scope;

        public HomeController(ILifetimeScope scope)
        {
            _scope = scope;
        }
        public ActionResult Index()
        {
            var model = _scope.Resolve<AllCategoryModel>();
            model.GetCategories();
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}