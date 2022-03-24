using OSL.Forum.Core.Contexts;
using EO = OSL.Forum.Core.Entities;
using OSL.Forum.DataAccessLayer.Repositories;
using System;

namespace OSL.Forum.Core.Repositories
{
    public class ForumRepository : Repository<EO.Forum, Guid>, IForumRepository
    {
        public ForumRepository(ICoreDbContext dbContext)
            : base((CoreDbContext)dbContext)
        {
        }
    }
}