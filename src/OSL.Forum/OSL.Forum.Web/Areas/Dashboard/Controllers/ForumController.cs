using Autofac;
using log4net;
using OSL.Forum.Web.Areas.Dashboard.Models.Forum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OSL.Forum.Web.Areas.Dashboard.Controllers
{
    [Authorize(Roles ="SuperAdmin, Admin, Moderator")]
    public class ForumController : Controller
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(ForumController));
        private readonly ILifetimeScope _scope;

        public ForumController(ILifetimeScope scope)
        {
            _scope = scope;
        }

        [Authorize(Roles = "SuperAdmin, Admin")]
        public ActionResult CreateForum(Guid? id)
        {
            if (id == null)
                return RedirectToAction("Index", "Category");
            var model = _scope.Resolve<CreateForumModel>();
            model.Load(Guid.Parse(id.ToString()));
            return View(model);
        }

        [Authorize(Roles = "SuperAdmin, Admin")]
        public ActionResult EditForum(Guid? id)
        {
            if (id == null)
                return RedirectToAction("Index", "Category");
            var model = _scope.Resolve<EditForumModel>();
            model.Load(Guid.Parse(id.ToString()));
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin, Admin")]
        public async Task<ActionResult> CreateForum(CreateForumModel model)
        {
            if(model.CategoryId == null)
                return RedirectToAction("Index", "Category");

            if (ModelState.IsValid)
            {
                try
                {
                    if (!User.IsInRole("SuperAdmin") && !User.IsInRole("Admin"))
                        throw new UnauthorizedAccessException("You are not allowed to do this.");
                    model.Resolve(_scope);
                    await model.Create();
                    return RedirectToAction("Details", "Category", new { id = model.CategoryId });
                }
                catch (Exception ex)
                {
                    model.Load(model.CategoryId);
                    ModelState.AddModelError("", ex.Message);
                    _logger.Error("New Forum Creation failed.");
                    _logger.Error(ex.Message);
                    return View(model);
                }
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin, Admin")]
        public async Task<ActionResult> EditForum(EditForumModel model)
        {
            if (model.CategoryId == null)
                return RedirectToAction("Index", "Category");

            if (ModelState.IsValid)
            {
                try
                {
                    if (!User.IsInRole("SuperAdmin") && !User.IsInRole("Admin"))
                        throw new UnauthorizedAccessException("You are not allowed to do this.");
                    model.Resolve(_scope);
                    await model.Edit();
                    return RedirectToAction("Details", "Category", new { id = model.CategoryId });
                }
                catch (Exception ex)
                {
                    model.Load(model.Id);
                    ModelState.AddModelError("", ex.Message);
                    _logger.Error("Edit forum name failed.");
                    _logger.Error(ex.Message);
                    return View(model);
                }
            }
            return View();
        }
    }
}