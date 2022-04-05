using Autofac;
using AutoMapper;
using OSL.Forum.Core.Services;
using OSL.Forum.Core.Utilities;
using OSL.Forum.Web.Services;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BO = OSL.Forum.Core.BusinessObjects;

namespace OSL.Forum.Web.Areas.Dashboard.Models.Forum
{
    public class CreateForumModel
    {
        [Required]
        [Display(Name = "Forum Name")]
        [StringLength(64, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 5)]
        public string Name { get; set; }
        [Required]
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        private ILifetimeScope _scope;
        private IForumService _forumService;
        private ICategoryService _categoryService;
        private IMapper _mapper;
        private IDateTimeUtility _dateTimeUtility;
        private IProfileService _profileService;

        public CreateForumModel()
        {
        }

        public CreateForumModel(IForumService forumService, ICategoryService categoryService,
            IMapper mapper, IDateTimeUtility dateTimeUtility, IProfileService profileService)
        {
            _forumService = forumService;
            _mapper = mapper;
            _dateTimeUtility = dateTimeUtility;
            _profileService = profileService;
            _categoryService = categoryService;
        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _forumService = _scope.Resolve<IForumService>();
            _mapper = _scope.Resolve<IMapper>();
            _dateTimeUtility = _scope.Resolve<IDateTimeUtility>();
            _profileService = _scope.Resolve<IProfileService>();
            _categoryService = _scope.Resolve<ICategoryService>();
        }

        public void Load(Guid categoryId)
        {
            if (categoryId == Guid.Empty)
                throw new ArgumentNullException(nameof(categoryId));

            var category = _categoryService.GetCategory(categoryId);

            if (category == null)
                throw new InvalidOperationException("Category not found");
            
            CategoryName = category.Name;
            CategoryId = category.Id;            
        }
        public async Task Create()
        {
            var user = _profileService.GetUser();

            if (user == null)
                throw new InvalidOperationException("No user found for create a forum");

            var roles = await _profileService.UserRolesAsync();
            
            if(!roles.Contains("SuperAdmin") && !roles.Contains("Admin"))
                throw new InvalidOperationException("You are not allowed to create a forum");

            var time = _dateTimeUtility.Now;
            var forum = new BO.Forum()
            {
                Name = Name,
                CategoryId = CategoryId,
                ApplicationUserId = user.Id,
                CreationDate = time,
                ModificationDate = time,
            };

            _forumService.CreateForum(forum);
        }
    }
}