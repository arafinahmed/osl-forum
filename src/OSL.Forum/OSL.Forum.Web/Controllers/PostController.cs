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
                if(!model.Owner)
                    return RedirectToAction("TopicDetails", "Home", new { id = model.Topic.Id });
                return View(model);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [Authorize, HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(EditPostModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                model.Resolve(_scope);
                model.Edit();
                return RedirectToAction("TopicDetails", "Home", new { id = model.Topic.Id });
            }
            catch (Exception ex)
            {
                _logger.Error("Invalid attempt in url.");
                _logger.Error(ex.Message);
                return View(model);
            }
        }

        [Authorize, HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id)
        {
            var model = _scope.Resolve<DeletePostModel>();
            try
            {
                await model.Delete(id);
                if (model.TopicId == Guid.Empty)
                    return RedirectToAction("Index", "Home");
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("TopicDetails", "Home", new { id = model.TopicId });
            

        }

        [Authorize]
        public ActionResult Reply(Guid? id)
        {
            if (id == null)
                return RedirectToAction("Index", "Home");
            try
            {
                var model = _scope.Resolve<ReplyPostModel>();
                model.Load(Guid.Parse(id.ToString()));
                return View(model);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [Authorize, HttpPost, ValidateAntiForgeryToken]
        public ActionResult Reply(ReplyPostModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                model.Resolve(_scope);
                model.CreatePost();
                return RedirectToAction("TopicDetails", "Home", new { id = model.TopicId });
            }
            catch (Exception ex)
            {
                _logger.Error("Invalid attempt in url.");
                _logger.Error(ex.Message);
                return View(model);
            }
        }

        [Authorize, HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteFromProfile(Guid id)
        {
            try
            {
                var model = _scope.Resolve<DeletePostModel>();
                await model.Delete(id);

                if (model.TopicId == Guid.Empty)
                    return RedirectToAction("Index", "Home");
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Me", "Profile");
        }
    }
}