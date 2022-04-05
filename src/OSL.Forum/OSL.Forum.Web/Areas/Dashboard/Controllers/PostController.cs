using Autofac;
using log4net;
using OSL.Forum.Web.Areas.Dashboard.Models.AssignRole;
using OSL.Forum.Web.Areas.Dashboard.Models.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OSL.Forum.Web.Areas.Dashboard.Controllers
{
    [Authorize(Roles = "SuperAdmin, Admin, Moderator")]
    public class PostController : Controller
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(PostController));
        private readonly ILifetimeScope _scope;

        public PostController(ILifetimeScope scope)
        {
            _scope = scope;
        }

        public ActionResult Pending()
        {
            return View();
        }
    }
}