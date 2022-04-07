﻿using AutoMapper;
using BO = OSL.Forum.Core.BusinessObjects;
using EO = OSL.Forum.Core.Entities;
using OSL.Forum.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OSL.Forum.Core.Enums;

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

        public BO.Post GetPost(Guid postId)
        {
            if (postId == Guid.Empty)
                throw new ArgumentNullException(nameof(postId));

            var postEntity = _unitOfWork.Posts.Get(p => p.Id == postId, "").FirstOrDefault();

            if (postEntity == null)
                return null;

            var post = _mapper.Map<BO.Post>(postEntity);

            return post;
        }

        public void EditPost(BO.Post post)
        {
            if (post is null)
                throw new ArgumentNullException(nameof(post));

            var postEntity = _unitOfWork.Posts.GetById(post.Id);

            if (postEntity is null)
                throw new InvalidOperationException("Post is not found.");

            postEntity.Name = post.Name;
            postEntity.Description = post.Description;
            postEntity.ModificationDate = post.ModificationDate;

            _unitOfWork.Save();
        }

        public void Delete(Guid postId)
        {
            if (postId == Guid.Empty)
                throw new ArgumentNullException(nameof(postId));

            _unitOfWork.Posts.Remove(postId);
            _unitOfWork.Save();
        }

        public IList<BO.Post> GetPostByUser(string userId)
        {
            var postsEntity = _unitOfWork.Posts.Get(p => p.ApplicationUserId == userId, "");

            var posts = postsEntity.Select(post =>
                _mapper.Map<BO.Post>(post)
                ).ToList();

            return posts;
        }

        public (IList<BO.Post> Posts, int totalCount) GetPostByUser(string userId, int pageSize, int pageIndex)
        {
            var postsEntity = _unitOfWork.Posts.Get(p => p.ApplicationUserId == userId, q => q.OrderByDescending(c => c.ModificationDate), "", pageIndex, pageSize, false);

            var posts = postsEntity.data.Select(post =>
                _mapper.Map<BO.Post>(post)
                ).ToList();

            var totalCount = _unitOfWork.Posts.GetCount(p => p.ApplicationUserId == userId);

            return (posts, totalCount);
        }

        public IList<BO.Post> GetPendingPosts()
        {
            var postsEntity = _unitOfWork.Posts.Get(p => p.Status == Status.Pending.ToString(), "");

            var posts = postsEntity.Select(post =>
                _mapper.Map<BO.Post>(post)
                ).ToList();

            return posts;
        }

        public void ApprovePost(Guid postId)
        {
            if (postId == null)
                throw new ArgumentNullException("Post ID not provided");

            var post = _unitOfWork.Posts.GetById(postId);
            if (post == null)
                throw new InvalidOperationException("No Post found.");

            post.Status = Status.Approved.ToString();
            _unitOfWork.Save();
        }

        public void RejectPost(Guid postId)
        {
            if (postId == null)
                throw new ArgumentNullException("Post ID not provided");

            var post = _unitOfWork.Posts.GetById(postId);
            if (post == null)
                throw new InvalidOperationException("No Post found.");

            post.Status = Status.Rejected.ToString();
            _unitOfWork.Save();
        }
    }
}