﻿using Autofac;
using log4net;
using OSL.Forum.Web.Models.Category;
using OSL.Forum.Web.Models.Forum;
using OSL.Forum.Web.Models.Post;
using OSL.Forum.Web.Models.Topic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OSL.Forum.Web.Controllers
{
    public class PostController : Controller
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(PostController));
        private readonly ILifetimeScope _scope;

        public PostController(ILifetimeScope scope)
        {
            _scope = scope;
        }
        
        [Authorize]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
                return RedirectToAction("Index", "Home");
            try
            {
                var model = _scope.Resolve<EditPostModel>();
                model.Load(Guid.Parse(id.ToString()));
                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }
    }
}