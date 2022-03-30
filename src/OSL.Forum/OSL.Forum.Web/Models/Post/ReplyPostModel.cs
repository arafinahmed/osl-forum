using Autofac;
using OSL.Forum.Core.Services;
using OSL.Forum.Core.Utilities;
using OSL.Forum.Web.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BO = OSL.Forum.Core.BusinessObjects;
using System.Web.Mvc;
using OSL.Forum.Core.Enums;

namespace OSL.Forum.Web.Models.Post
{
    public class ReplyPostModel
    {
        [Required]
        [Display(Name = "Subject")]
        [StringLength(64, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 5)]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        [Display(Name = "Description")]
        [StringLength(10000, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 50)]
        public string Description { get; set; }
        public string ForumName { get; set; }
        public string CategoryName { get; set; }
        public Guid CategoryId { get; set; }
        public Guid ForumId { get; set; }
        public string TopicName { get; set; }
        public Guid TopicId { get; set; }
        private ILifetimeScope _scope;
        private ICategoryService _categoryService;
        private IForumService _forumService;
        private ITopicService _topicService;
        private IPostService _postService;
        private IDateTimeUtility _dateTimeUtility;
        private IProfileService _profileService;

        public ReplyPostModel()
        {
        }

        public ReplyPostModel(ICategoryService categoryService, IForumService forumService,
            ITopicService topicService, IPostService postService,
            IDateTimeUtility dateTimeUtility, IProfileService profileService)
        {
            _categoryService = categoryService;
            _forumService = forumService;
            _topicService = topicService;
            _postService = postService;
            _dateTimeUtility = dateTimeUtility;
            _profileService = profileService;
        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _categoryService = _scope.Resolve<ICategoryService>();
            _forumService = _scope.Resolve<IForumService>();
            _topicService = _scope.Resolve<ITopicService>();
            _postService = _scope.Resolve<IPostService>();
            _dateTimeUtility = _scope.Resolve<IDateTimeUtility>();
            _profileService = _scope.Resolve<IProfileService>();
        }

        public void Load(Guid topicId)
        {
            if (topicId == Guid.Empty)
                throw new ArgumentNullException("Topic Id not provided.");
            
            var topic = _topicService.GetTopic(topicId);

            if (topic == null)
                throw new InvalidOperationException("Topic not found");


            var forum = _forumService.GetForum(topic.ForumId);
            var category = _categoryService.GetCategory(forum.CategoryId);

            CategoryName = category.Name;
            ForumName = forum.Name;
            CategoryId = category.Id;
            ForumId = forum.Id;
            TopicName = topic.Name;
            TopicId = topic.Id;
        }

        public void CreatePost()
        {
            var user = _profileService.GetUser();

            if (user == null)
                throw new InvalidOperationException("No user found");

            var time = _dateTimeUtility.Now;
            var post = new BO.Post
            {
                Name = Name,
                Description = Description,
                CreationDate = time,
                ModificationDate = time,
                ApplicationUserId = user.Id,
                TopicId = TopicId,
                Status = Status.Pending.ToString()
            };

            _postService.CreatePost(post);
        }
    }
}