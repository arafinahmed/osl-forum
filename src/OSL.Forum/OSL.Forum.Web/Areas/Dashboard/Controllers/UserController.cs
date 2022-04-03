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
    [Authorize(Roles ="SuperAdmin, Admin")]
    public class UserController : Controller
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(UserController));
        private readonly ILifetimeScope _scope;

        public UserController(ILifetimeScope scope)
        {
            _scope = scope;
        }
    }
}