﻿using System;
using System.Collections.Generic;
using BO = OSL.Forum.Core.BusinessObjects;

namespace OSL.Forum.Core.Services
{
    public interface IForumService
    {
        void CreateForum(BO.Forum forum);
    }
}