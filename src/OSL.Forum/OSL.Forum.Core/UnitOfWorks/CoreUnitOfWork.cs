﻿using OSL.Forum.Core.Contexts;
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

        public CoreUnitOfWork(ICoreDbContext context,
            ICategoryRepository categories, IForumRepository forums
        ) : base((DbContext)context)
        {
            Categories = categories;
            Forums = forums;
        }
    }
}
