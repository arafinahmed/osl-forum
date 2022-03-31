using OSL.Forum.Core.Repositories;
using OSL.Forum.DataAccessLayer.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSL.Forum.Core.UnitOfWorks
{
    public interface ICoreUnitOfWork : IUnitOfWork
    {
        ICategoryRepository Categories { get; }
        IForumRepository Forums { get; }
        ITopicRepository Topics { get; }
        IPostRepository Posts { get; }
        IFavoriteForumRepository FavoriteForums { get; }
    }
}
