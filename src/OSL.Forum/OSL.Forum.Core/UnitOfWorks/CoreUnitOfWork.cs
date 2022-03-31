using OSL.Forum.Core.Contexts;
using OSL.Forum.Core.Repositories;
using OSL.Forum.DataAccessLayer.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSL.Forum.Core.UnitOfWorks
{
    public class CoreUnitOfWork : UnitOfWork, ICoreUnitOfWork
    {
        public ICategoryRepository Categories { get; private set; }
        public IForumRepository Forums { get; private set; }
        public ITopicRepository Topics { get; private set; }
        public IPostRepository Posts { get; private set; }
        public IFavoriteForumRepository FavoriteForums { get; private set; }

        public CoreUnitOfWork(ICoreDbContext context,
            ICategoryRepository categories, IForumRepository forums, ITopicRepository topics,
            IPostRepository posts, IFavoriteForumRepository favoriteForums
        ) : base((DbContext)context)
        {
            Categories = categories;
            Forums = forums;
            Topics = topics;
            Posts = posts;
            FavoriteForums = favoriteForums;
        }
    }
}
