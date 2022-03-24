using AutoMapper;
using OSL.Forum.Core.BusinessObjects;
using OSL.Forum.Web.Areas.Dashboard.Models.Category;

namespace OSL.Forum.Web.Profiles
{
    public class WebProfile : Profile
    {
        public WebProfile()
        {
            CreateMap<CreateCategoryModel, Category>().ReverseMap();
            CreateMap<EditCategoryModel, Category>().ReverseMap();
            CreateMap<CategoryDetailsModel, Category>().ReverseMap();
        }
    }
}