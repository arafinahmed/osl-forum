using Autofac;
using AutoMapper;
using OSL.Forum.Core.Services;
using OSL.Forum.Core.Utilities;
using System.ComponentModel.DataAnnotations;
using BO = OSL.Forum.Core.BusinessObjects;

namespace OSL.Forum.Web.Areas.Dashboard.Models.Forum
{
    public class CreateForumModel
    {
        [Required]
        [Display(Name = "Forum Name")]
        [StringLength(64, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 5)]
        public string Name { get; set; }
        private ILifetimeScope _scope;
        private IForumService _forumService;
        private IMapper _mapper;
        private IDateTimeUtility _dateTimeUtility;

        public CreateForumModel()
        {
        }

        public CreateForumModel(IForumService forumService,
            IMapper mapper, IDateTimeUtility dateTimeUtility)
        {
            _forumService = forumService;
            _mapper = mapper;
            _dateTimeUtility = dateTimeUtility;
        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _forumService = _scope.Resolve<IForumService>();
            _mapper = _scope.Resolve<IMapper>();
            _dateTimeUtility = _scope.Resolve<IDateTimeUtility>();
        }
    }
}