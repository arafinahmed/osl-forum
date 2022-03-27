using AutoMapper;
using BO = OSL.Forum.Core.BusinessObjects;
using OSL.Forum.Web.Areas.Dashboard.Models.Category;
using OSL.Forum.Web.Areas.Dashboard.Models.Forum;
using OSL.Forum.Web.Models.Category;
using OSL.Forum.Web.Models.Forum;

namespace OSL.Forum.Web.Profiles
{
    public class WebProfile : Profile
    {
        public WebProfile()
        {
            CreateMap<CreateCategoryModel, BO.Category>().ReverseMap();
            CreateMap<EditCategoryModel, BO.Category>().ReverseMap();
            CreateMap<CategoryDetailsModel, BO.Category>().ReverseMap();
            CreateMap<EditForumModel, BO.Forum>().ReverseMap();
            CreateMap<CategoryModel, BO.Category>().ReverseMap();
            CreateMap<ForumModel, BO.Forum>().ReverseMap();
        }
    }
}