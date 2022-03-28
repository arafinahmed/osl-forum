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
    public class PostService : IPostService
    {
        private readonly ICoreUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PostService(ICoreUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void CreatePost(BO.Post post)
        {
            if (post is null)
                throw new ArgumentNullException(nameof(post));

            var postEntity = _mapper.Map<EO.Post>(post);

            _unitOfWork.Posts.Add(postEntity);
            _unitOfWork.Save();
        }
    }
}