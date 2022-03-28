using OSL.Forum.Core.Contexts;
using EO = OSL.Forum.Core.Entities;
using OSL.Forum.DataAccessLayer.Repositories;
using System;

namespace OSL.Forum.Core.Repositories
{
    public class PostRepository : Repository<EO.Post, Guid>, IPostRepository
    {
        public PostRepository(ICoreDbContext dbContext)
            : base((CoreDbContext)dbContext)
        {
        }
    }
}