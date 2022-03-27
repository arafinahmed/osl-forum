using Autofac;
using OSL.Forum.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OSL.Forum.Web.Areas.Dashboard.Models.Forum
{
    public class DeleteForumModel
    {
        private ILifetimeScope _scope;
        private IForumService _forumService;

        public DeleteForumModel() { }

        public DeleteForumModel(IForumService forumService)
        {
            _forumService = forumService;
        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _forumService = _scope.Resolve<IForumService>();
        }

        public void Delete(Guid categoryId)
        {
            _forumService.Delete(categoryId);
        }
    }
}