using AutoMapper;
using BO = OSL.Forum.Core.BusinessObjects;
using OSL.Forum.Web.Areas.Dashboard.Models.Category;
using OSL.Forum.Web.Areas.Dashboard.Models.Forum;

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
        }
    }
}