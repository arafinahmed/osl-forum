using Autofac;
using AutoMapper;
using OSL.Forum.Core.Services;
using OSL.Forum.Core.Utilities;
using System.Collections.Generic;
using BO = OSL.Forum.Core.BusinessObjects;

namespace OSL.Forum.Web.Areas.Dashboard.Models.Category
{
    public class CategoriesListModel
    {
        public Pager Pager { get; set; }
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

        public void GetCategories(int? page)
        {
            var res = _categoryService.GetCategories(10, page ?? 1);
            Pager = new Pager(res.totalCount, page ?? 1);
            Categories = res.categories;
        }
    }
}