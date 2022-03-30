using Autofac;
using AutoMapper;
using OSL.Forum.Core.Services;
using OSL.Forum.Core.Utilities;
using OSL.Forum.Web.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BO = OSL.Forum.Core.BusinessObjects;

namespace OSL.Forum.Web.Models.Post
{
    public class EditPostModel
    {
        public Guid Id { get; set; }
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
        public BO.Category Category { get; set; }
        public BO.Forum Forum { get; set; }
        public BO.Topic Topic { get; set; }

        private ILifetimeScope _scope;
        private ICategoryService _categoryService;
        private IForumService _forumService;
        private ITopicService _topicService;
        private IPostService _postService;
        private IDateTimeUtility _dateTimeUtility;
        private IProfileService _profileService;

        public EditPostModel()
        {
        }

        public EditPostModel(ICategoryService categoryService, IForumService forumService,
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

        public void Load(Guid postId)
        {
            if (postId == Guid.Empty)
                throw new ArgumentNullException(nameof(postId));

            var post = _postService.GetPost(postId);
            
            if (post == null)
                throw new NullReferenceException("Post not found with the post id");

            Id = post.Id;
            Name = post.Name;
            Description = post.Description;

            Topic = _topicService.Get(post.TopicId);
            Forum = _forumService.GetForum(Topic.ForumId);
            Category = _categoryService.GetCategory(Forum.CategoryId);
        }
    }
}