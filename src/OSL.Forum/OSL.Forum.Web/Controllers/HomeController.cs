using Autofac;
using log4net;
using OSL.Forum.Web.Models.Category;
using OSL.Forum.Web.Models.Forum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OSL.Forum.Web.Controllers
{
    public class HomeController : Controller
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(HomeController));
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

        public ActionResult Category(Guid? id)
        {
            if (id == null)
                return RedirectToAction("Index", "Home");
            try
            {
                var model = _scope.Resolve<CategoryModel>();
                model.Load(Guid.Parse(id.ToString()));
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.Error("Invalid attempt in url.");
                _logger.Error(ex.Message);
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Forum(Guid? id)
        {
            if (id == null)
                return RedirectToAction("Index", "Home");
            try
            {
                var model = _scope.Resolve<ForumModel>();
                model.Load(Guid.Parse(id.ToString()));
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.Error("Invalid attempt in url.");
                _logger.Error(ex.Message);
                return RedirectToAction("Index", "Home");
            }
        }
    }
}