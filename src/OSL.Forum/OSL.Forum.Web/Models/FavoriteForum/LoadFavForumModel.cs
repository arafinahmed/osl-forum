using Autofac;
using OSL.Forum.Core.Services;
using OSL.Forum.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using BO = OSL.Forum.Core.BusinessObjects;

namespace OSL.Forum.Web.Models.FavoriteForum
{
    public class LoadFavForumModel
    {
        public IList<BO.FavoriteForum> FavoriteForums { get; set; }
        private IFavoriteForumService _favoriteForumService;
        private IProfileService _profileService;
        private ILifetimeScope _scope;

        public LoadFavForumModel()
        {

        }

        public LoadFavForumModel(IFavoriteForumService favoriteForumService, IProfileService profileService)
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

        public void Load()
        {
            var user = _profileService.GetUser();
            if (user != null)
            {
                FavoriteForums = _favoriteForumService.GetUserFavoriteForums(user.Id);
            }
        }
    }
}