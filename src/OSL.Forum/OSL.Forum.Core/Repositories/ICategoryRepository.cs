using OSL.Forum.Core.Entities;
using OSL.Forum.DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSL.Forum.Core.Repositories
{
    public interface ICategoryRepository : IRepository<Category, Guid>
    {
    }
}