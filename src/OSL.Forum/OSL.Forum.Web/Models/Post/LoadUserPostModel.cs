using Autofac;
using OSL.Forum.Core.Services;
using OSL.Forum.Core.Utilities;
using OSL.Forum.Web.Services;
using System.Collections;
using System.Collections.Generic;
using BO = OSL.Forum.Core.BusinessObjects;

namespace OSL.Forum.Web.Models.Post
{
    public class LoadUserPostModel
    {
        public Pager Pager { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public IList<BO.Post> Posts { get; set; }
        private ILifetimeScope _scope;
        private IPostService _postService;
        private IProfileService _profileService;


        public LoadUserPostModel()
        {
        }

        public LoadUserPostModel(IPostService postService, IProfileService profileService)
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

        public void Load()
        {
            Pager = new Pager(25, 1);
            ApplicationUser = _profileService.GetUser();
            Posts = new List<BO.Post>();
            if (ApplicationUser != null)
            {
                Posts = _postService.GetPostByUser(ApplicationUser.Id);
            }
        }
    }
}