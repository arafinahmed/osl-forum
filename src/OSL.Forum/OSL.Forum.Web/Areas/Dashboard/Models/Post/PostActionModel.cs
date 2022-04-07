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
        private IForumService _forumService;
        private ICategoryService _categoryService;

        public PostActionModel()
        {
        }

        public PostActionModel(IPostService postService, IProfileService profileService, ITopicService topicService, IForumService forumService,
            ICategoryService categoryService)
        {
            _postService = postService;
            _profileService = profileService;
            _topicService = topicService;
            _forumService = forumService;
            _categoryService = categoryService;
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

        public void Load(Guid postId)
        {
            if (postId == null)
                throw new ArgumentNullException("Post Id not provided");

            Post = _postService.GetPost(postId);

            if (Post == null)
                throw new InvalidOperationException("Post not found");

            Post.Topic = _topicService.GetTopic(Post.TopicId);
            Post.Topic.Forum = _forumService.GetForum(Post.Topic.ForumId);
            Post.Topic.Forum.Category = _categoryService.GetCategory(Post.Topic.Forum.CategoryId);

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