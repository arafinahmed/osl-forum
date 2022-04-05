using Autofac;
using AutoMapper;
using OSL.Forum.Web.Models;
using OSL.Forum.Web.Seeds;
using OSL.Forum.Web.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OSL.Forum.Web.Areas.Dashboard.Models.AssignRole
{
    public class AssignRoleModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Role")]

        public string UserRole { get; set; }
        public List<SelectListItem> AdminRoleList { get; set; }
        public List<SelectListItem> SuperAdminRoleList { get; set; }
        private ILifetimeScope _scope;
        private IProfileService _profileService;
        private IMapper _mapper;

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
            var user = _profileService.GetUserByEmail(Email);
            if (user == null)
                throw new InvalidOperationException($"No user found with the email {Email}");

            var role = await _profileService.UserRolesByEmailAsync(Email);
            var assignerRole = await _profileService.UserRolesAsync();
            if (assignerRole.Contains(Roles.SuperAdmin.ToString()) && UserRole != Roles.SuperAdmin.ToString() && !role.Contains(Roles.SuperAdmin.ToString()))
            {
                await _profileService.AddUserToRoleAsync(new ApplicationUserRole { UserId = user.Id, UserRole = UserRole });
            }
            else if (assignerRole.Contains(Roles.Admin.ToString()) && UserRole != Roles.Admin.ToString() && UserRole != Roles.SuperAdmin.ToString() && !role.Contains(Roles.Admin.ToString()))
            {
                await _profileService.AddUserToRoleAsync(new ApplicationUserRole { UserId = user.Id, UserRole = UserRole });
            }
            else
                throw new InvalidOperationException("You are not permitted to assign role.");
        }
    }
}