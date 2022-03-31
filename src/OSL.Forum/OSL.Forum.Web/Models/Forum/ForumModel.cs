using Autofac;
using AutoMapper;
using OSL.Forum.Core.Services;
using OSL.Forum.Core.Utilities;
using OSL.Forum.Web.Services;
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
        public bool IsFavourite { get; set; }
        public Guid CategoryId { get; set; }
        private ILifetimeScope _scope;
        private ICategoryService _categoryService;
        private IForumService _forumService;
        private IFavoriteForumService _favoriteForumService;
        private IMapper _mapper;
        private IProfileService _profileService;
        public IList<BO.Topic> Topics { get; set; }

        public ForumModel()
        {
        }

        public ForumModel(ICategoryService categoryService, IForumService forumService,
            IMapper mapper, IFavoriteForumService favoriteForumService, IProfileService profileService)
        {
            _categoryService = categoryService;
            _mapper = mapper;
            _forumService = forumService;
            _favoriteForumService = favoriteForumService;
            _profileService = profileService;
        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _categoryService = _scope.Resolve<ICategoryService>();
            _mapper = _scope.Resolve<IMapper>();
            _forumService = _scope.Resolve<IForumService>();
            _favoriteForumService = _scope.Resolve<IFavoriteForumService>();
            _profileService = _scope.Resolve<IProfileService>();
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

            var user = _profileService.GetUser();

            if (user == null)
                IsFavourite = false;
            else
                IsFavourite = _favoriteForumService.IsFavourite(forumId, user.Id);

            CategoryName = category.Name;
        }
    }
}