using Autofac;
using OSL.Forum.Core.Services;
using OSL.Forum.Web.Services;
using System;
using BO = OSL.Forum.Core.BusinessObjects;

namespace OSL.Forum.Web.Models.FavoriteForum
{
    public class FavoriteForumModel
    {
        private IFavoriteForumService _favoriteForumService;
        private IProfileService _profileService;
        private ILifetimeScope _scope;

        public FavoriteForumModel()
        {

        }

        public FavoriteForumModel(IFavoriteForumService favoriteForumService, IProfileService profileService)
        {
            _favoriteForumService = favoriteForumService;
            _profileService = profileService;
        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _profileService = _scope.Resolve<IProfileService>();
            _favoriteForumService = _scope.Resolve<IFavoriteForumService>();
        }

        public void AddToFavorite(Guid forumId)
        {
            var user = _profileService.GetUser();
            _favoriteForumService.AddToFavorite(new BO.FavoriteForum { ForumId = forumId, ApplicationUserId = user.Id});
        }

        public void RemoveFromFavorite(Guid forumId)
        {
            var user = _profileService.GetUser();
            _favoriteForumService.RemoveFromFavorite(new BO.FavoriteForum { ForumId = forumId, ApplicationUserId = user.Id });
        }
    }
}