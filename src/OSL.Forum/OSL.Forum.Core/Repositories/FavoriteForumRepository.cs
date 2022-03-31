using OSL.Forum.Core.Contexts;
using OSL.Forum.Core.Entities;
using OSL.Forum.DataAccessLayer.Repositories;
using System;


namespace OSL.Forum.Core.Repositories
{
    public class FavoriteForumRepository : Repository<FavoriteForum, Guid>, IFavoriteForumRepository
    {
        public FavoriteForumRepository(ICoreDbContext dbContext)
            : base((CoreDbContext)dbContext)
        {
        }
    }
}