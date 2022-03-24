using AutoMapper;
using EO = OSL.Forum.Core.Entities;
using BO = OSL.Forum.Core.BusinessObjects;

namespace OSL.Forum.Core.Profiles
{
    public class CoreProfiles : Profile
    {
        public CoreProfiles()
        {
            CreateMap<EO.Category, BO.Category>().ReverseMap();
            CreateMap<EO.Forum, BO.Forum>().ReverseMap();
        }
    }
}
