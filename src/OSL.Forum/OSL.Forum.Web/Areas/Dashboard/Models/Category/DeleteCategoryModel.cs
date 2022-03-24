using Autofac;
using OSL.Forum.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OSL.Forum.Web.Areas.Dashboard.Models.Category
{
    public class DeleteCategoryModel
    {
        private ILifetimeScope _scope;
        private ICategoryService _categoryService;

        public DeleteCategoryModel() { }

        public DeleteCategoryModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _categoryService = _scope.Resolve<ICategoryService>();
        }

        public void Delete(Guid categoryId)
        {
            _categoryService.Delete(categoryId);
        }
    }
}