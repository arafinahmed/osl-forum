using BO = OSL.Forum.Core.BusinessObjects;

namespace OSL.Forum.Core.Services
{
    public interface ICategoryService
    {
        void CreateCategory(BO.Category category);
    }
}
