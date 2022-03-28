using Autofac;
using AutoMapper;
using OSL.Forum.Core.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        public Guid Id { get; set; }
        public string ForumName { get; set; }
        public string CategoryName { get; set; }
        public Guid CategoryId { get; set; }
        public Guid ForumId { get; set; }
        private ILifetimeScope _scope;
        private ICategoryService _categoryService;
        private IForumService _forumService;

        public NewTopicModel()
        {
        }

        public NewTopicModel(ICategoryService categoryService, IForumService forumService)
        {
            _categoryService = categoryService;
            _forumService = forumService;
        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _categoryService = _scope.Resolve<ICategoryService>();
            _forumService = _scope.Resolve<IForumService>();
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
    }
}