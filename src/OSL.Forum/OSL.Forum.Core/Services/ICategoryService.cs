using System;
using System.Collections.Generic;
using BO = OSL.Forum.Core.BusinessObjects;

namespace OSL.Forum.Core.Services
{
    public interface ICategoryService
    {
        void CreateCategory(BO.Category category);
        IList<BO.Category> GetCategories();
        BO.Category GetCategory(Guid categoryId);
        void EditCategory(BO.Category category);
        void Delete(Guid categoryId);
    }
}
