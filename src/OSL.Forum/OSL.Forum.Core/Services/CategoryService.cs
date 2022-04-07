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

        public (IList<BO.Category> categories, int totalCount) GetCategories(int pageSize, int pageIndex)
        {
            var categoryEntities = _unitOfWork.Categories.Get(null, q => q.OrderByDescending(c => c.ModificationDate), "Forums", pageIndex, pageSize, true);

            var categoryList = from c in categoryEntities.data
                               orderby c.ModificationDate descending
                               select c;

            var categories = new List<BO.Category>();

            foreach (var entity in categoryList)
            {
                entity.Forums = entity.Forums.OrderByDescending(c => c.ModificationDate).Take(4).ToList();
                var category = _mapper.Map<BO.Category>(entity);
                categories.Add(category);
            }

            var count = _unitOfWork.Categories.GetCount();

            return (categories, count);
        }

        public BO.Category GetCategory(Guid categoryId)
        {
            if (categoryId == Guid.Empty)
                throw new ArgumentNullException("No category found with the category id.");

            var categoryEntity = _unitOfWork.Categories.GetById(categoryId);
            var forums = _unitOfWork.Forums.Get(x => x.CategoryId == categoryId, "");

            if (categoryEntity == null)
                return null;

            categoryEntity.Forums = forums.OrderByDescending(c => c.ModificationDate).ToList();
            var category = _mapper.Map<BO.Category>(categoryEntity);

            return category;
        }

        public void EditCategory(BO.Category category)
        {
            if (category is null)
                throw new ArgumentNullException(nameof(category));

            var oldEntity = _unitOfWork.Categories.Get(x => x.Name == category.Name, "").FirstOrDefault();

            if (oldEntity != null)
                throw new DuplicateNameException("Category Name Already Exist.");

            var categoryEntity = _unitOfWork.Categories.GetById(category.Id);

            if (categoryEntity is null)
                throw new InvalidOperationException("Category is not found.");

            categoryEntity.Name = category.Name;
            categoryEntity.ModificationDate = category.ModificationDate;

            _unitOfWork.Save();
        }

        public void Delete(Guid categoryId)
        {
            if (categoryId == Guid.Empty)
                throw new ArgumentNullException(nameof(categoryId));

            _unitOfWork.Categories.Remove(categoryId);
            _unitOfWork.Save();
        }
    }
}