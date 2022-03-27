using AutoMapper;
using BO = OSL.Forum.Core.BusinessObjects;
using EO = OSL.Forum.Core.Entities;
using OSL.Forum.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSL.Forum.Core.Services
{
    public class ForumService : IForumService
    {
        private readonly ICoreUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ForumService(ICoreUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void CreateForum(BO.Forum forum)
        {
            if (forum is null)
                throw new ArgumentNullException(nameof(forum));

            var oldForum = _unitOfWork.Forums.Get(f => f.Name == forum.Name && f.CategoryId == forum.CategoryId, "").FirstOrDefault();

            if (oldForum != null)
                throw new DuplicateNameException("This Forum name already exists under this category.");

            var forumEntity = _mapper.Map<EO.Forum>(forum);

            _unitOfWork.Forums.Add(forumEntity);
            _unitOfWork.Save();
        }

        public void EditForum(BO.Forum forum)
        {
            if (forum is null)
                throw new ArgumentNullException(nameof(forum));

            var categoryEntity = _unitOfWork.Categories.GetById(forum.CategoryId);

            if (categoryEntity == null)
                throw new InvalidOperationException("Forum can not be created without valid category id.");

            var oldForum = _unitOfWork.Forums.Get(f => f.Name == forum.Name && f.CategoryId == forum.CategoryId, "").FirstOrDefault();

            if (oldForum != null)
                throw new DuplicateNameException("This Forum name already exists under this category.");

            var forumEntity = _unitOfWork.Forums.GetById(forum.Id);

            if (forumEntity is null)
                throw new InvalidOperationException("Forum is not found.");

            forumEntity.Name = forum.Name;
            forumEntity.ModificationDate = forum.ModificationDate;

            _unitOfWork.Save();
        }

        public BO.Forum GetCategory(Guid forumId)
        {
            if (forumId == Guid.Empty)
                throw new ArgumentNullException("Forum id is empty.");

            var forumEntity = _unitOfWork.Forums.GetById(forumId);

            if (forumEntity == null)
                return null;

            return _mapper.Map<BO.Forum>(forumEntity);
        }
    }
}