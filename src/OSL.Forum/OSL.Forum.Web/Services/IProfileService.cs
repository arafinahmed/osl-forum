using OSL.Forum.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OSL.Forum.Web.Services
{
    public interface IProfileService
    {
        string UserID();
        ApplicationUser GetUser();
        ApplicationUser GetUser(string userId);
        Task<IList<string>> UserRolesAsync();
        bool Owner(string userId);
        bool IsAuthenticated();
        Task EditProfileAsync(ApplicationUser applicationUser);
        List<SelectListItem> GetUserList();
        Task AddUserToRoleAsync(ApplicationUserRole applicationUserRole);
        Task<IList<string>> UserRolesAsync(string userId);
        ApplicationUser GetUserByEmail(string email);
        Task<IList<string>> UserRolesByEmailAsync(string email);
    }
}
