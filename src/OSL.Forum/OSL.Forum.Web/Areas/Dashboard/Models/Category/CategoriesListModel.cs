using Autofac;
using AutoMapper;
using OSL.Forum.Core.Services;
using System.Collections.Generic;
using BO = OSL.Forum.Core.BusinessObjects;

namespace OSL.Forum.Web.Areas.Dashboard.Models.Category
{
    public class CategoriesListModel
    {
        public IList<BO.Category> Categories;
        private ILifetimeScope _scope;
        private ICategoryService _categoryService;
        public CategoriesListModel()
        {
        }

        public CategoriesListModel(ICategoryService categoryService)
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