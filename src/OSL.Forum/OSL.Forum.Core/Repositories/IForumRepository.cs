using EO = OSL.Forum.Core.Entities;
using OSL.Forum.DataAccessLayer.Repositories;
using System;


namespace OSL.Forum.Core.Repositories
{
    public interface IForumRepository : IRepository<EO.Forum, Guid>
    {
    }
}