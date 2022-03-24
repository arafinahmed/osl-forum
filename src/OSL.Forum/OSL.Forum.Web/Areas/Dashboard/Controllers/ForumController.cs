using Autofac;
using log4net;
using OSL.Forum.Web.Areas.Dashboard.Models.Category;
using OSL.Forum.Web.Areas.Dashboard.Models.Forum;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public ActionResult CreateForum(Guid id)
        {
            var model = _scope.Resolve<CreateForumModel>();
            model.Load(id);
            return View(model);
        }
    }
}