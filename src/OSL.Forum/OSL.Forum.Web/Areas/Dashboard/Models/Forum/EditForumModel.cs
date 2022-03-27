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
    public class EditForumModel
    {
        [Required]
        [Display(Name = "Forum Name")]
        [StringLength(64, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 5)]
        public string Name { get; set; }
        [Required]
        public Guid CategoryId { get; set; }
        [Required]
        public Guid Id { get; set; }
        public string CategoryName { get; set; }
        private ILifetimeScope _scope;
        private IForumService _forumService;
        private ICategoryService _categoryService;
        private IMapper _mapper;
        private IDateTimeUtility _dateTimeUtility;
        private IProfileService _profileService;

        public EditForumModel()
        {
        }

        public EditForumModel(IForumService forumService, ICategoryService categoryService,
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

        public void Load(Guid forumId)
        {
            if (forumId == Guid.Empty)
                throw new ArgumentNullException(nameof(forumId));

            var forum = _forumService.GetCategory(forumId);
            _mapper.Map(forum, this);

            var category = _categoryService.GetCategory(forum.CategoryId);

            if (category == null)
                throw new InvalidOperationException("Category not found");
            
            CategoryName = category.Name;
        }
        public async Task Edit()
        {
            var user = _profileService.GetUser();

            if (user == null)
                throw new InvalidOperationException("No user found for edit a forum");

            var roles = await _profileService.UserRoles();
            
            if(!roles.Contains("SuperAdmin") && !roles.Contains("Admin"))
                throw new InvalidOperationException("You are not allowed to create a forum");

            var forum = new BO.Forum()
            {
                Id = Id,
                Name = Name,
                CategoryId = CategoryId,
                ApplicationUserId = user.Id,
                ModificationDate = _dateTimeUtility.Now
        };

            _forumService.EditForum(forum);
        }
    }
}