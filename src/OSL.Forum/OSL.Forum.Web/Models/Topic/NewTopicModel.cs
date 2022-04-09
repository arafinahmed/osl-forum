using Autofac;
using OSL.Forum.Core.Services;
using OSL.Forum.Core.Utilities;
using OSL.Forum.Web.Services;
using System;
using BO = OSL.Forum.Core.BusinessObjects;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OSL.Forum.Core.Enums;

namespace OSL.Forum.Web.Models.Topic
{
    public class NewTopicModel
    {
        [Required]
        [Display(Name = "Topic Name")]
        [StringLength(64, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 5)]
        public string Subject { get; set; }
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
        private ILifetimeScope _scope;
        private ICategoryService _categoryService;
        private IForumService _forumService;
        private ITopicService _topicService;
        private IPostService _postService;
        private IDateTimeUtility _dateTimeUtility;
        private IProfileService _profileService;

        public NewTopicModel()
        {
        }

        public NewTopicModel(ICategoryService categoryService, IForumService forumService,
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

        public void Load(Guid forumId)
        {
            if (forumId == Guid.Empty)
                throw new ArgumentNullException(nameof(forumId));

            var forum = _forumService.GetForum(forumId);

            var category = _categoryService.GetCategory(forum.CategoryId);

            if (category == null)
                throw new InvalidOperationException("Category not found");

            CategoryName = category.Name;
            ForumName = forum.Name;
            CategoryId = category.Id;
            ForumId = forum.Id;
        }

        public void AddTopic()
        {
            var user = _profileService.GetUser();

            if (user == null)
                throw new InvalidOperationException("No user found");

            var time = _dateTimeUtility.Now;
            var topic = new BO.Topic
            {
                Name = Subject,
                CreationDate = time,
                ModificationDate = time,
                ApplicationUserId = user.Id,
                ForumId = ForumId,
                ApprovalType = ApprovalType.Manual.ToString(),
                Status = TopicStatus.Open.ToString()
            };
            var topicId = _topicService.Create(topic);

            var post = new BO.Post()
            {
                Name = Subject,
                Description = Description,
                CreationDate = time,
                ModificationDate = time,
                ApplicationUserId = user.Id,
                TopicId = topicId,
                Status = Status.Pending.ToString()
            };
            
            _postService.CreatePost(post);
        }
    }
}