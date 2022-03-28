using EO = OSL.Forum.Core.Entities;
using OSL.Forum.DataAccessLayer.Repositories;
using System;


namespace OSL.Forum.Core.Repositories
{
    public interface ITopicRepository : IRepository<EO.Topic, Guid>
    {
    }
}