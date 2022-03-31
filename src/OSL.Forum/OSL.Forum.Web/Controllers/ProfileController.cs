using Autofac;
using log4net;
using OSL.Forum.Web.Models.Category;
using OSL.Forum.Web.Models.Forum;
using OSL.Forum.Web.Models.Post;
using OSL.Forum.Web.Models.Topic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OSL.Forum.Web.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(ProfileController));
        private readonly ILifetimeScope _scope;

        public ProfileController(ILifetimeScope scope)
        {
            _scope = scope;
        }

        public ActionResult Me()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = _scope.Resolve<LoadUserPostModel>();
            model.Load();
            return View(model);
        }
    }
}