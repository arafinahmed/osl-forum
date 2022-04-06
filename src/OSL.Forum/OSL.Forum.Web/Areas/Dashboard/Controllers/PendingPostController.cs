using Autofac;
using log4net;
using OSL.Forum.Web.Areas.Dashboard.Models.AssignRole;
using OSL.Forum.Web.Areas.Dashboard.Models.Category;
using OSL.Forum.Web.Areas.Dashboard.Models.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OSL.Forum.Web.Areas.Dashboard.Controllers
{
    [Authorize(Roles = "SuperAdmin, Admin, Moderator")]
    public class PendingPostController : Controller
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(PendingPostController));
        private readonly ILifetimeScope _scope;

        public PendingPostController(ILifetimeScope scope)
        {
            _scope = scope;
        }

        public async Task<ActionResult> Pending()
        {
            var model = _scope.Resolve<LoadPendingPostsModel>();
            await model.Load();
            return View(model);
        }

        public ActionResult Approve(Guid? id)
        {
            if(id == null)
                return RedirectToAction("Pending");
            
            var model = _scope.Resolve<ApprovePostModel>();
            model.Approve(Guid.Parse(id.ToString()));
            
            return RedirectToAction("Pending");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Reject(Guid? id)
        {
            if (id == null)
                return RedirectToAction("Pending", "PendingPost", new { area = "Dashboard"});

            var model = _scope.Resolve<RejectPostModel>();
            model.Reject(Guid.Parse(id.ToString()));

            return RedirectToAction("Pending", "PendingPost", new { area = "Dashboard" });
        }

        public ActionResult Post(Guid? id)
        {
            if (id == null)
                return RedirectToAction("Index", "Category", new { area = "Dashboard" });

            try
            {
                var model = _scope.Resolve<PostActionModel>();
                model.Load(Guid.Parse(id.ToString()));
                return View(model);
            }
            catch
            {
                return RedirectToAction("Index", "Category", new { area = "Dashboard" });
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult PostAction(string button, PostActionModel model)
        {
            if (!ModelState.IsValid)
            {
                if (model.Post.Id != null)
                    return RedirectToAction("Post", "PendingPost", new { area = "Dashboard", id = model.Post.Id });
                else
                    return RedirectToAction("Index", "Category", new { area = "Dashboard" });
            }

            model.Resolve(_scope);
            try
            {
                model.StatusUpdate(button);
                return RedirectToAction("Pending", "PendingPost", new { area = "Dashboard" });
            }
            catch
            {
                return RedirectToAction("Index", "Category", new { area = "Dashboard" });
            }
        }
    }
}