using Autofac;
using OSL.Forum.Core.Services;
using OSL.Forum.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace OSL.Forum.Web.Models.Post
{
    public class DeletePostModel
    {
        private ILifetimeScope _scope;
        private IPostService _postService;
        private IProfileService _profileService;
        public Guid TopicId { get; set; }

        public DeletePostModel() { }

        public DeletePostModel(IPostService postService, IProfileService profileService)
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

        public async Task Delete(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException("Id is empty.");
            
            var post = _postService.GetPost(id);
            if (post == null)
                throw new InvalidOperationException("No post is found to delte.");
            TopicId = post.TopicId;
            
            var owner = _profileService.Owner(post.ApplicationUserId);
            var roles = await _profileService.UserRoles();

            if (!roles.Contains("SuperAdmin") && !roles.Contains("Admin") && !roles.Contains("Moderator") && !owner)
                throw new InvalidOperationException("You are not allowed to delete a post.");

            _postService.Delete(id);
        }
    }
}