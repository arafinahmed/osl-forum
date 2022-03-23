using AutoMapper;
using BO = OSL.Forum.Core.BusinessObjects;
using EO = OSL.Forum.Core.Entities;
using OSL.Forum.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSL.Forum.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICoreUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(ICoreUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void CreateCategory(BO.Category category)
        {
            if (category == null)
                throw new ArgumentNullException("Category can not be empty.");

            var categoryEntity = _unitOfWork.Categories.Get(x => x.Name == category.Name, "").FirstOrDefault();

            if (categoryEntity != null)
                throw new DuplicateNameException("Category Name Already Exist.");
            
            var newcategoryEntity = _mapper.Map<EO.Category>(category);
            _unitOfWork.Categories.Add(newcategoryEntity);
            _unitOfWork.Save();
        }

        public IList<BO.Category> GetCategories()
        {
            var categoryEntities = _unitOfWork.Categories.Get(null, "");

            var categoryList = from c in categoryEntities
                               orderby c.ModificationDate descending
                               select c;

            var categories = new List<BO.Category>();

            foreach (var entity in categoryList)
            {
                var category = _mapper.Map<BO.Category>(entity);
                categories.Add(category);
            }

            return categories;
        }
    }
}