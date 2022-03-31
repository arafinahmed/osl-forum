using System;
using System.Collections.Generic;
using BO = OSL.Forum.Core.BusinessObjects;

namespace OSL.Forum.Core.Services
{
    public interface IFavoriteForumService
    {
        void AddToFavorite(BO.FavoriteForum favoriteForum);
        bool IsFavourite(Guid forumId, string userId);
        void RemoveFromFavorite(BO.FavoriteForum favoriteForum);
    }
}
