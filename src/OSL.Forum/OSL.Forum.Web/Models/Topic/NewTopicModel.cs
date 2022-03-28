using Autofac;
using AutoMapper;
using OSL.Forum.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OSL.Forum.Web.Models.Topic
{
    public class NewTopicModel
    {
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