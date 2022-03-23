using OSL.Forum.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSL.Forum.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICoreUnitOfWork _unitOfWork;

        public CategoryService(ICoreUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
