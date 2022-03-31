﻿using System;
using System.Collections.Generic;
using BO = OSL.Forum.Core.BusinessObjects;

namespace OSL.Forum.Core.Services
{
    public interface IPostService
    {
        void CreatePost(BO.Post post);
        BO.Post GetPost(Guid postId);
        void EditPost(BO.Post post);
        void Delete(Guid postId);
        IList<BO.Post> GetPostByUser(string userId);
    }
}
