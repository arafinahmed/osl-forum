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

        public void Delete(Guid forumId)
        {
            _forumService.Delete(forumId);
        }

        public Guid GetCategoryId(Guid forumId)
        {
            if (forumId == null)
                throw new ArgumentNullException("No forum id is provided");

            var forum = _forumService.GetForum(forumId);

            if (forum == null)
                throw new NullReferenceException("No forum found.");

            return forum.CategoryId;
        }
    }
}