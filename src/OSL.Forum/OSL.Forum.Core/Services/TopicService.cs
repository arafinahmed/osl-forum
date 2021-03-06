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
using OSL.Forum.Core.Enums;

namespace OSL.Forum.Core.Services
{
    public class TopicService : ITopicService
    {
        private readonly ICoreUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TopicService(ICoreUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Guid Create(BO.Topic topic)
        {
            if (topic is null)
                throw new ArgumentNullException(nameof(topic));

            var oldTopic = _unitOfWork.Topics.Get(f => f.Name == topic.Name && f.ForumId == topic.ForumId, "").FirstOrDefault();

            if (oldTopic != null)
                throw new DuplicateNameException("This topic name already exists under this forum.");

            var topicEntity = _mapper.Map<EO.Topic>(topic);

            _unitOfWork.Topics.Add(topicEntity);
            _unitOfWork.Save();

            return topicEntity.Id;
        }

        public BO.Topic GetTopic(Guid topicId)
        {
            if (topicId == Guid.Empty)
                throw new ArgumentNullException(nameof(topicId));

            var topicEntity = _unitOfWork.Topics.GetById(topicId);
            var postListEntity = _unitOfWork.Posts.Get(p => p.TopicId == topicId && p.Status == Status.Approved.ToString(), "");

            if (topicEntity == null)
                return null;
            
            var postList = new List<BO.Post>();
            var topic = _mapper.Map<BO.Topic>(topicEntity);

            foreach(var post in postListEntity)
            {
                var postBO = _mapper.Map<BO.Post>(post);
                postList.Add(postBO);
            }

            topic.Posts = postList;
            return topic;
        }

        public BO.Topic Get(Guid topicId)
        {
            if (topicId == Guid.Empty)
                throw new ArgumentNullException(nameof(topicId));

            var topicEntity = _unitOfWork.Topics.GetDynamic(t => t.Id == topicId, null, "", false).FirstOrDefault();

            if (topicEntity == null)
                return null;

            var topic = _mapper.Map<BO.Topic>(topicEntity);

            return topic;
        }

        public void AutoApprovalOn(Guid topicId)
        {
            if (topicId == Guid.Empty)
                throw new ArgumentNullException(nameof(topicId));

            var topicEntity = _unitOfWork.Topics.GetDynamic(t => t.Id == topicId, null, "", false).FirstOrDefault();

            if (topicEntity == null)
                throw new InvalidOperationException("No topic found with the topic id");

            topicEntity.ApprovalType = ApprovalType.Auto.ToString();
            _unitOfWork.Save();
        }

        public void DeleteTopic(Guid topicId)
        {
            if (topicId == Guid.Empty)
                throw new ArgumentNullException("No topic id is provided.");

            _unitOfWork.Topics.Remove(topicId);
            _unitOfWork.Save();
        }

        public void ChangeTopicStatus(Guid topicId, TopicStatus status)
        {
            if (topicId == Guid.Empty)
                throw new ArgumentNullException("No topic id is provided.");

            var topicEntity = _unitOfWork.Topics.GetById(topicId);
            
            if (topicEntity == null)
                throw new InvalidOperationException("No topic found with the provided topic id.");

            topicEntity.Status = status.ToString();
            _unitOfWork.Save();
        }
    }
}