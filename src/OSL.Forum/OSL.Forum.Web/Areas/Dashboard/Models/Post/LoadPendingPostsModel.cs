using System;
using BO = OSL.Forum.Core.BusinessObjects;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OSL.Forum.Web.Services;
using OSL.Forum.Core.Services;
using Autofac;
using System.Threading.Tasks;
using OSL.Forum.Web.Seeds;

namespace OSL.Forum.Web.Areas.Dashboard.Models.Post
{
    public class LoadPendingPostsModel
    {
        public IList<BO.Post> Posts { get; set; }
        public BO.Topic Topic { get; set; }
        private IPostService _postService;
        private IProfileService _profileService;
        private ITopicService _topicService;
        private IForumService _forumService;
        private ICategoryService _categoryService;
        private ILifetimeScope _scope;

        public LoadPendingPostsModel()
        {
        }

        public LoadPendingPostsModel(IPostService postService, IProfileService profileService, ITopicService topicService,
            IForumService forumService, ICategoryService categoryService)
        {
            _postService = postService;
            _profileService = profileService;
            _topicService = topicService;
            _categoryService = categoryService;
            _forumService = forumService;
        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _postService = _scope.Resolve<IPostService>();
            _profileService = _scope.Resolve<IProfileService>();
            _topicService = _scope.Resolve<ITopicService>();
            _forumService = _scope.Resolve<IForumService>();
            _categoryService = _scope.Resolve<ICategoryService>();
        }

        public async Task Load()
        {
            var roles = await _profileService.UserRolesAsync();
            Posts = new List<BO.Post>();
            var postList = new List<BO.Post>();
            if (roles.Contains(Roles.Admin.ToString()) || roles.Contains(Roles.SuperAdmin.ToString()) || roles.Contains(Roles.SuperAdmin.ToString()))
            {
                var posts = _postService.GetPendingPosts();
                foreach (var post in posts)
                {
                    post.OwnerName = _profileService.GetUser(post.ApplicationUserId).Name;
                    postList.Add(post);
                }
            }
            Posts = postList;

            foreach(var post in Posts)
            {
                post.Topic = _topicService.GetTopic(post.TopicId);
                post.Topic.Forum = _forumService.GetForum(post.Topic.ForumId);
                post.Topic.Forum.Category = _categoryService.GetCategory(post.Topic.Forum.CategoryId);
            }
            
        }
    }
}