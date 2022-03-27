using Autofac;
using OSL.Forum.Core.Services;
using System.Collections.Generic;
using BO = OSL.Forum.Core.BusinessObjects;

namespace OSL.Forum.Web.Models.Category
{
    public class AllCategoryModel
    {
        public IList<BO.Category> Categories;
        private ILifetimeScope _scope;
        private ICategoryService _categoryService;
        public AllCategoryModel()
        {
        }

        public AllCategoryModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _categoryService = _scope.Resolve<ICategoryService>();
        }

        public void GetCategories()
        {
            Categories = _categoryService.GetCategories();
        }
    }
}