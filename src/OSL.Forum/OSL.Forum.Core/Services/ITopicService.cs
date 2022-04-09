using System;
using System.Collections.Generic;
using BO = OSL.Forum.Core.BusinessObjects;

namespace OSL.Forum.Core.Services
{
    public interface ITopicService
    {
        Guid Create(BO.Topic topic);
        BO.Topic GetTopic(Guid topicId);
        BO.Topic Get(Guid topicId);
        void AutoApprovalOn(Guid topicId);
        void DeleteTopic(Guid topicId);
        void CloseTopic(Guid topicId);
    }
}
