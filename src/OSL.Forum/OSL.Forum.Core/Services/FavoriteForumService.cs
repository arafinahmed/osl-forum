using AutoMapper;
using BO = OSL.Forum.Core.BusinessObjects;
using EO = OSL.Forum.Core.Entities;
using OSL.Forum.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSL.Forum.Core.Services
{
    public class FavoriteForumService : IFavoriteForumService
    {
        private readonly ICoreUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FavoriteForumService(ICoreUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void AddToFavorite(BO.FavoriteForum favoriteForum)
        {
            if (favoriteForum == null)
                throw new ArgumentNullException("Favourite Forum not provided.");

            var oldFavoriteForum = _unitOfWork.FavoriteForums.Get(x => x.ApplicationUserId == favoriteForum.ApplicationUserId && x.ForumId == favoriteForum.ForumId, "").FirstOrDefault();

            if (oldFavoriteForum != null)
                throw new InvalidOperationException("The Forum is already in your Favorite list.");

            var favoriteForumEntity = _mapper.Map<EO.FavoriteForum>(favoriteForum);
            _unitOfWork.FavoriteForums.Add(favoriteForumEntity);
            _unitOfWork.Save();
        }

        public bool IsFavourite(Guid forumId, string userId)
        {
            var favoriteForumEntity = _unitOfWork.FavoriteForums
                .Get(ff => ff.ForumId == forumId && ff.ApplicationUserId == userId, "").FirstOrDefault();

            if (favoriteForumEntity == null)
                return false;

            return true;
        }

        public void RemoveFromFavorite(BO.FavoriteForum favoriteForum)
        {
            if (favoriteForum == null)
                throw new ArgumentNullException("Favourite Forum not provided.");

            var oldFavoriteForum = _unitOfWork.FavoriteForums.Get(x => x.ApplicationUserId == favoriteForum.ApplicationUserId && x.ForumId == favoriteForum.ForumId, "").FirstOrDefault();

            if (oldFavoriteForum == null)
                throw new InvalidOperationException("The Forum is not in your Favorite list.");

            _unitOfWork.FavoriteForums.Remove(oldFavoriteForum.Id);
            _unitOfWork.Save();
        }

        public List<BO.FavoriteForum> GetUserFavoriteForums(string userId)
        {
            if (userId == string.Empty)
                return null;

            var favoriteForumsEntity = _unitOfWork.FavoriteForums
                .Get(ff => ff.ApplicationUserId == userId, "");

            var favoriteForums = favoriteForumsEntity.Select(favoriteForum =>
                _mapper.Map<BO.FavoriteForum>(favoriteForum)
                ).ToList();

            return favoriteForums;
        }
    }
}