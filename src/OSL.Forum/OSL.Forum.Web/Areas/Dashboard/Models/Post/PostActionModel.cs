using Autofac;
using OSL.Forum.Core.Services;
using OSL.Forum.Web.Services;
using System;
using BO = OSL.Forum.Core.BusinessObjects;

namespace OSL.Forum.Web.Areas.Dashboard.Models.Post
{
    public class PostActionModel
    {
        public bool IsAutoApprove { get; set; }
        public BO.Post Post { get; set; }
        private IPostService _postService;
        private ILifetimeScope _scope;
        private IProfileService _profileService;
        private ITopicService _topicService;

        public PostActionModel()
        {
        }

        public PostActionModel(IPostService postService, IProfileService profileService, ITopicService topicService)
        {
            _postService = postService;
            _profileService = profileService;
            _topicService = topicService;
        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _postService = _scope.Resolve<IPostService>();
            _profileService = _scope.Resolve<IProfileService>();
            _topicService = _scope.Resolve<ITopicService>();
        }

        public void Load(Guid postId)
        {
            if (postId == null)
                throw new ArgumentNullException("Post Id not provided");

            Post = _postService.GetPost(postId);

            if (Post == null)
                throw new InvalidOperationException("Post not found");

            Post.OwnerName = _profileService.GetUser(Post.ApplicationUserId).Name;
        }

        public void StatusUpdate(string status)
        {
            if (status == "Approve")
                _postService.ApprovePost(Post.Id);
            else if (status == "Reject")
                _postService.RejectPost(Post.Id);

            if (IsAutoApprove)
                _topicService.AutoApprovalOn(Post.TopicId);
        }
    }
}