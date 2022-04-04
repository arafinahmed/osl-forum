using Autofac;
using AutoMapper;
using OSL.Forum.Web.Models;
using OSL.Forum.Web.Seeds;
using OSL.Forum.Web.Services;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OSL.Forum.Web.Areas.Dashboard.Models.AssignRole
{
    public class AssignRoleModel
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string UserRole { get; set; }
        public List<SelectListItem> ApplicationUserList { get; set; }
        public List<SelectListItem> AdminRoleList { get; set; }
        public List<SelectListItem> SuperAdminRoleList { get; set; }
        private ILifetimeScope _scope;
        private IProfileService _profileService;
        private IMapper _mapper;

        public List<string> Demo = new List<string> { "One", "Two", "Three" };
        public AssignRoleModel()
        {
        }

        public AssignRoleModel(IProfileService profileService, IMapper mapper)
        {
            _profileService = profileService;
            _mapper = mapper;
        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _profileService = _scope.Resolve<IProfileService>();
            _mapper = _scope.Resolve<IMapper>();
            _profileService = _scope.Resolve<IProfileService>();
        }

        public void GetUsers()
        {
            ApplicationUserList = _profileService.GetUserList();
        }

        public void SuperAdminRoles()
        {
            SuperAdminRoleList = new List<SelectListItem>()
            {
                new SelectListItem
                {
                    Text = Roles.Admin.ToString(),
                    Value = Roles.Admin.ToString()
                }
            };

            SuperAdminRoleList.AddRange(AdminRoleList);

        }

        public void AdminRoles()
        {
            AdminRoleList = new List<SelectListItem>()
            {
                new SelectListItem
                {
                    Text = Roles.Moderator.ToString(),
                    Value = Roles.Moderator.ToString()
                },
                new SelectListItem
                {
                    Text = Roles.User.ToString(),
                    Value = Roles.User.ToString()
                },
            };
        }

        public async Task AddUserToRoleAsync()
        {
            var applicationUserRole = _mapper.Map<ApplicationUserRole>(this);

            await _profileService.AddUserToRoleAsync(applicationUserRole);
        }
    }
}