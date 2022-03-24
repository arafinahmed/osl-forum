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
        public ActionResult EditCategory(Guid? id)
        {
            if(id == null)
                return RedirectToAction("Index", "Category");
            var model = _scope.Resolve<EditCategoryModel>();
            model.Load(Guid.Parse(id.ToString()));
            return View(model);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public ActionResult EditCategory(EditCategoryModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (!User.IsInRole("SuperAdmin"))
                        throw new UnauthorizedAccessException("You are not allowed to do this.");
                    model.Resolve(_scope);
                    model.Edit();
                    return RedirectToAction("Index", "Category");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    _logger.Error("Category Edit failed.");
                    _logger.Error(ex.Message);
                    return View(model);
                }
            }
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Roles = "SuperAdmin")]
        public ActionResult Delete(Guid id)
        {
            if (!User.IsInRole("SuperAdmin"))
                return RedirectToAction(nameof(Index));
            var model = _scope.Resolve<DeleteCategoryModel>();
            model.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "SuperAdmin, Admin, Moderator")]
        public ActionResult Details(Guid? id)
        {
            if(id == null)
                return RedirectToAction("Index", "Category");
            try
            {
                var model = _scope.Resolve<CategoryDetailsModel>();
                model.Load(Guid.Parse(id.ToString()));
                return View(model);
            }
            catch(Exception ex)
            {
                _logger.Error("Invalid attempt in url.");
                _logger.Error(ex.Message);
                return RedirectToAction("Index", "Category");
            }
        }
    }
}