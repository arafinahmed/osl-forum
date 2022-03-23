using Autofac;
using log4net;
using OSL.Forum.Web.Areas.Dashboard.Models.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OSL.Forum.Web.Areas.Dashboard.Controllers
{
    [Authorize(Roles ="SuperAdmin, Admin, Moderator")]
    public class CategoryController : Controller
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(CategoryController));
        private readonly ILifetimeScope _scope;

        public CategoryController(ILifetimeScope scope)
        {
            _scope = scope;
        }

        // GET: Dashboard/Category
        public ActionResult Index()
        {
            var model = _scope.Resolve<CategoriesListModel>();
            model.GetCategories();
            return View(model);
        }

        [Authorize(Roles = "SuperAdmin")]
        public ActionResult CreateCategory()
        {
            return View();
        }

        [Authorize(Roles = "SuperAdmin")]
        public ActionResult EditCategory()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public ActionResult CreateCategory(CreateCategoryModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (!User.IsInRole("SuperAdmin"))
                        throw new UnauthorizedAccessException("You are not allowed to do this.");
                    model.Resolve(_scope);
                    model.Create();
                    return Redirect(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    _logger.Error("New Category Creation failed.");
                    _logger.Error(ex.Message);
                    return View(model);
                }
            }
            return View();
        }
    }
}