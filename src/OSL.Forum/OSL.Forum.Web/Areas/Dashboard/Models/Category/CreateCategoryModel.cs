using Autofac;
using AutoMapper;
using OSL.Forum.Core.Services;
using BO = OSL.Forum.Core.BusinessObjects;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using OSL.Forum.Core.Utilities;

namespace OSL.Forum.Web.Areas.Dashboard.Models.Category
{
    public class CreateCategoryModel
    {
        [Required]
        [Display(Name = "Category Name")]
        [StringLength(64, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 5)]
        public string Name { get; set; }
        private ILifetimeScope _scope;
        private ICategoryService _categoryService;
        private IMapper _mapper;
        private IDateTimeUtility _dateTimeUtility;

        public CreateCategoryModel()
        {
        }

        public CreateCategoryModel(ICategoryService categoryService,
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

        public void Create()
        {
            var category = _mapper.Map<BO.Category>(this);
            category.CreationDate = _dateTimeUtility.Now;
            category.ModificationDate = _dateTimeUtility.Now;
            _categoryService.CreateCategory(category);
        }
    }
}