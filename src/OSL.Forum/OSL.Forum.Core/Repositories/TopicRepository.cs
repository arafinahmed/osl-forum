using OSL.Forum.Core.Contexts;
using EO = OSL.Forum.Core.Entities;
using OSL.Forum.DataAccessLayer.Repositories;
using System;

namespace OSL.Forum.Core.Repositories
{
    public class TopicRepository : Repository<EO.Topic, Guid>, ITopicRepository
    {
        public TopicRepository(ICoreDbContext dbContext)
            : base((CoreDbContext)dbContext)
        {
        }
    }
}