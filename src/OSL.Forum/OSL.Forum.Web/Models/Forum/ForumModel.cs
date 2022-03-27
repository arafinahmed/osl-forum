using Autofac;
using AutoMapper;
using OSL.Forum.Core.Services;
using OSL.Forum.Core.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using BO = OSL.Forum.Core.BusinessObjects;

namespace OSL.Forum.Web.Models.Forum
{
    public class ForumModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public Guid CategoryId { get; set; }
        private ILifetimeScope _scope;
        private ICategoryService _categoryService;
        private IForumService _forumService;
        private IMapper _mapper;

        public ForumModel()
        {
        }

        public ForumModel(ICategoryService categoryService, IForumService forumService,
            IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
            _forumService = forumService;
        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _categoryService = _scope.Resolve<ICategoryService>();
            _mapper = _scope.Resolve<IMapper>();
            _forumService = _scope.Resolve<IForumService>();
        }

        public void Load(Guid forumId)
        {
            if (forumId == Guid.Empty)
                throw new ArgumentNullException(nameof(forumId));

            var forum = _forumService.GetForum(forumId);
            _mapper.Map(forum, this);

            var category = _categoryService.GetCategory(forum.CategoryId);

            if (category == null)
                throw new InvalidOperationException("Category not found");

            CategoryName = category.Name;
        }
    }
}