using Autofac;
using OSL.Forum.Core.Services;
using OSL.Forum.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OSL.Forum.Web.Areas.Dashboard.Models.Post
{
    public class ApprovePostModel
    {
        private IPostService _postService;
        private ILifetimeScope _scope;

        public ApprovePostModel()
        {
        }

        public ApprovePostModel(IPostService postService)
        {
            _postService = postService;
        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _postService = _scope.Resolve<IPostService>();
        }

        public void Approve(Guid postId)
        {
            if (postId == null)
                throw new ArgumentNullException("Post id not provided.");

            _postService.ApprovePost(postId);
        }
    }
}