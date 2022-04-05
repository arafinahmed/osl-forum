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
        private IPostService _postService;
        private IProfileService _profileService;
        private ILifetimeScope _scope;

        public LoadPendingPostsModel()
        {
        }

        public LoadPendingPostsModel(IPostService postService, IProfileService profileService)
        {
            _postService = postService;
            _profileService = profileService;
        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _postService = _scope.Resolve<IPostService>();
            _profileService = _scope.Resolve<IProfileService>();
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
        }
    }
}