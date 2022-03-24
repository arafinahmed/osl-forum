﻿using OSL.Forum.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSL.Forum.Web.Services
{
    public interface IProfileService
    {
        string UserID();
        ApplicationUser GetUser();
        ApplicationUser GetUser(string userId);
        Task<IList<string>> UserRoles();
        bool Owner(string userId);
        bool IsAuthenticated();
    }
}
