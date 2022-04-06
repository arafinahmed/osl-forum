using Autofac;
using OSL.Forum.Core.Services;
using OSL.Forum.Core.Utilities;
using OSL.Forum.Web.Services;
using System.Collections.Generic;
using System.Linq;
using BO = OSL.Forum.Core.BusinessObjects;

namespace OSL.Forum.Web.Models.Category
{
    public class AllCategoryModel
    {
        public Pager Pager { get; set; }
        public IList<BO.Category> Categories;
        public IList<BO.FavoriteForum> FavoriteForums { get; set; }
        private ILifetimeScope _scope;
        private ICategoryService _categoryService;
        private IFavoriteForumService _favoriteForumService;
        private IProfileService _profileService;
        public AllCategoryModel()
        {
        }

        public AllCategoryModel(ICategoryService categoryService, IFavoriteForumService favoriteForumService, IProfileService profileService)
        {
            _categoryService = categoryService;
            _favoriteForumService = favoriteForumService;
            _profileService = profileService;
        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _categoryService = _scope.Resolve<ICategoryService>();
            _profileService = _scope.Resolve<IProfileService>();
            _favoriteForumService = _scope.Resolve<IFavoriteForumService>();
        }

        public void GetCategories(int? page)
        {
            var pageIndex = page ?? 1;
            var res = _categoryService.GetCategories(10, pageIndex);
            Categories = res.categories;
            var count = res.totalCount;
            FavoriteForums = new List<BO.FavoriteForum>();
            var user = _profileService.GetUser();
            Pager = new Pager(count, page);
            if(user != null)
            {
                FavoriteForums = _favoriteForumService.GetUserFavoriteForums(user.Id).Take(4).ToList();
            }
            
            
        }
    }
}