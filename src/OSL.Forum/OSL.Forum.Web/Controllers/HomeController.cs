using Autofac;
using log4net;
using OSL.Forum.Web.Models.Category;
using OSL.Forum.Web.Models.FavoriteForum;
using OSL.Forum.Web.Models.Forum;
using OSL.Forum.Web.Models.Topic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OSL.Forum.Web.Controllers
{
    public class HomeController : Controller
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(HomeController));
        private readonly ILifetimeScope _scope;

        public HomeController(ILifetimeScope scope)
        {
            _scope = scope;
        }
        public ActionResult Index(int? page)
        {
            page = page ?? 1;
            if (page < 1)
                page = 1;

            var model = _scope.Resolve<AllCategoryModel>();
            model.GetCategories(page);
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Category(Guid? id)
        {
            if (id == null)
                return RedirectToAction("Index", "Home");
            try
            {
                var model = _scope.Resolve<CategoryModel>();
                model.Load(Guid.Parse(id.ToString()));
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.Error("Invalid attempt in url.");
                _logger.Error(ex.Message);
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Forum(Guid? id)
        {
            if (id == null)
                return RedirectToAction("Index", "Home");
            try
            {
                var model = _scope.Resolve<ForumModel>();
                model.Load(Guid.Parse(id.ToString()));
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.Error("Invalid attempt in url.");
                _logger.Error(ex.Message);
                return RedirectToAction("Index", "Home");
            }
        }
        [Authorize]
        public ActionResult NewTopic(Guid? id)
        {
            if (id == null)
                return RedirectToAction("Index", "Home");
            try
            {
                var model = _scope.Resolve<NewTopicModel>();
                model.Load(Guid.Parse(id.ToString()));
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.Error("Invalid attempt in url.");
                _logger.Error(ex.Message);
                return RedirectToAction("Index", "Home");
            }
        }

        [Authorize, HttpPost, ValidateAntiForgeryToken]
        public ActionResult NewTopic(NewTopicModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            try
            {
                model.Resolve(_scope);
                model.AddTopic();
                return RedirectToAction("Forum", "Home", new { id = model.ForumId});
            }
            catch (Exception ex)
            {
                _logger.Error("Invalid attempt in url.");
                _logger.Error(ex.Message);
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult TopicDetails(Guid? id)
        {
            if (id == null)
                return RedirectToAction("Index", "Home");

            var model = _scope.Resolve<TopicDetailsModel>();
            
            try
            {
                model.Load(Guid.Parse(id.ToString()));
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.Error("Invalid attempt in url.");
                _logger.Error(ex.Message);
                return RedirectToAction("Index", "Home");
            }
        }

        [Authorize]
        public ActionResult AddToFavorite(Guid? id)
        {
            if (id == null)
                return RedirectToAction("Index", "Home");

            try
            {
                var model = _scope.Resolve<FavoriteForumModel>();
                model.AddToFavorite(Guid.Parse(id.ToString()));
                return RedirectToAction("Forum", "Home", new { id = id });
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [Authorize]
        public ActionResult RemoveFromFavorite(Guid? id)
        {
            if (id == null)
                return RedirectToAction("Index", "Home");

            try
            {
                var model = _scope.Resolve<FavoriteForumModel>();
                model.RemoveFromFavorite(Guid.Parse(id.ToString()));
                return RedirectToAction("Forum", "Home", new { id = id });
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [Authorize]
        public ActionResult FavouriteForums()
        {
            var model = _scope.Resolve<LoadFavForumModel>();
            model.Load();
            return View(model);
        }

        [Authorize, HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteTopic(Guid id)
        {
            try
            {
                var model = _scope.Resolve<DeleteTopicModel>();
                var forumId = await model.DeleteTopic(id);
                return RedirectToAction("Forum", "Home", new { id = forumId });
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [Authorize, HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> CloseTopic(Guid id)
        {
            try
            {
                var model = _scope.Resolve<ChangeTopicStatusModel>();
                var forumId = await model.CloseTopic(id);
                return RedirectToAction("Forum", "Home", new { id = forumId });
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}