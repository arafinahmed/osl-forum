using Autofac;
using OSL.Forum.Core.Services;
using OSL.Forum.Web.Services;
using System;
using System.Collections.Generic;
using BO = OSL.Forum.Core.BusinessObjects;

namespace OSL.Forum.Web.Models.Topic
{
    public class TopicDetailsModel
    {
        public string ForumName { get; set; }
        public string CategoryName { get; set; }
        public Guid CategoryId { get; set; }
        public Guid ForumId { get; set; }
        public BO.Topic Topic { get; set; }
        
        private ICategoryService _categoryService;
        private ITopicService _topicService;
        private IProfileService _profileService;
        private IForumService _forumService;
        
        public TopicDetailsModel()
        {
        }

        public TopicDetailsModel(ICategoryService categoryService,
            IProfileService profileService,
            ITopicService topicService, IForumService forumService)
        {
            _categoryService = categoryService;
            _profileService = profileService;
            _topicService = topicService;
            _forumService = forumService;
        }

        public void Load(Guid topicId)
        {
            if (topicId == Guid.Empty)
                throw new ArgumentNullException(nameof(topicId));

            Topic = _topicService.GetTopic(topicId);

            if (Topic == null)
                throw new InvalidOperationException("No topic found.");

            var forum = _forumService.GetForum(Topic.ForumId);

            if (forum == null)
                throw new InvalidOperationException("No forum found.");

            var category = _categoryService.GetCategory(forum.CategoryId);

            if (category == null)
                throw new InvalidOperationException("Category not found");

            CategoryName = category.Name;
            ForumName = forum.Name;
            CategoryId = category.Id;
            ForumId = forum.Id;

            var postList = new List<BO.Post>();

            foreach (var topicPost in Topic.Posts)
            {
                topicPost.Owner = _profileService.Owner(topicPost.ApplicationUserId);
                topicPost.OwnerName = _profileService.GetUser(topicPost.ApplicationUserId).Name;
                postList.Add(topicPost);                
            }
            Topic.Posts = postList;
        }
    }
}