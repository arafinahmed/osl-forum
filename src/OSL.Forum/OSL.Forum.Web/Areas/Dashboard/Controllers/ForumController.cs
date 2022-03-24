﻿using Autofac;
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
    public class ForumController : Controller
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(CategoryController));
        private readonly ILifetimeScope _scope;

        public ForumController(ILifetimeScope scope)
        {
            _scope = scope;
        }
    }
}