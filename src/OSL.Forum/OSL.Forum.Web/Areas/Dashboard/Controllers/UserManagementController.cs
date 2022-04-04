using Autofac;
using log4net;
using OSL.Forum.Web.Areas.Dashboard.Models.AssignRole;
using OSL.Forum.Web.Areas.Dashboard.Models.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OSL.Forum.Web.Areas.Dashboard.Controllers
{
    [Authorize(Roles ="SuperAdmin, Admin")]
    public class UserManagementController : Controller
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(UserManagementController));
        private readonly ILifetimeScope _scope;

        public UserManagementController(ILifetimeScope scope)
        {
            _scope = scope;
        }

        public ActionResult AssignRole()
        {
            var model = _scope.Resolve<AssignRoleModel>();
            model.GetUsers();
            model.AdminRoles();
            model.SuperAdminRoles();
            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> AssignRole(AssignRoleModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                model.Resolve(_scope);
                await model.AddUserToRoleAsync();
                return RedirectToAction("AssignRole", "UserManagement");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                _logger.Error("User role assign failed.");
                _logger.Error(ex.Message);

                return View(model);
            }
        }
    }
}