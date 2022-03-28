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
    }
}