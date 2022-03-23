using Autofac;
using AutoMapper;
using OSL.Forum.Core.Services;
using OSL.Forum.Core.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using BO = OSL.Forum.Core.BusinessObjects;

namespace OSL.Forum.Web.Areas.Dashboard.Models.Category
{
    public class EditCategoryModel
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        [Display(Name = "Category Name")]
        [StringLength(64, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 5)]
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        private ILifetimeScope _scope;
        private ICategoryService _categoryService;
        private IDateTimeUtility _dateTimeUtility;
        private IMapper _mapper;

        public EditCategoryModel()
        {
        }

        public EditCategoryModel(ICategoryService categoryService,
            IMapper mapper, IDateTimeUtility dateTimeUtility)
        {
            _categoryService = categoryService;
            _mapper = mapper;
            _dateTimeUtility = dateTimeUtility;
        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _categoryService = _scope.Resolve<ICategoryService>();
            _mapper = _scope.Resolve<IMapper>();
            _dateTimeUtility = _scope.Resolve<IDateTimeUtility>();
        }

        public void Load(Guid categoryId)
        {
            if (categoryId == Guid.Empty)
                throw new ArgumentNullException(nameof(categoryId));

            var category = _categoryService.GetCategory(categoryId);

            if (category == null)
                throw new InvalidOperationException("Category not found");

            _mapper.Map(category, this);
        }

        public void Edit()
        {
            var category = _mapper.Map<BO.Category>(this);
            category.ModificationDate = _dateTimeUtility.Now;
            _categoryService.EditCategory(category);
        }
    }
}