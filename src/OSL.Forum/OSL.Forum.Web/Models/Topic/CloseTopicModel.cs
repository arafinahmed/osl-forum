using Autofac;
using AutoMapper;
using OSL.Forum.Core.Enums;
using OSL.Forum.Core.Services;
using OSL.Forum.Web.Seeds;
using OSL.Forum.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace OSL.Forum.Web.Models.Topic
{
    public class CloseTopicModel
    {
        private ITopicService _topicService;
        private ILifetimeScope _scope;
        private IProfileService _profileService;

        public CloseTopicModel()
        {
        }

        public CloseTopicModel(ITopicService topicService, IProfileService profileService)
        {
            _topicService = topicService;
            _profileService = profileService;
        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _topicService = _scope.Resolve<ITopicService>();
            _profileService = _scope.Resolve<IProfileService>();
        }

        public async Task<Guid> CloseTopic(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException("No id is provided to delete topic.");

            var topic = _topicService.GetTopic(id);

            if (topic == null)
                throw new InvalidOperationException("No topic with the topic id");

            if (topic.Status == TopicStatus.Closed.ToString())
                throw new InvalidOperationException("Topic is already closed.");

            var owner = _profileService.Owner(topic.ApplicationUserId);
            if (owner)
            {
                _topicService.ChangeTopicStatus(id, TopicStatus.Closed);
                return topic.ForumId;
            }
            else
            {
                var roles = await _profileService.UserRolesAsync();
                if(roles.Contains(Roles.SuperAdmin.ToString()) || roles.Contains(Roles.SuperAdmin.ToString()) || roles.Contains(Roles.SuperAdmin.ToString()))
                {
                    _topicService.ChangeTopicStatus(id, TopicStatus.Closed);
                    return topic.ForumId;
                }
            }
            throw new InvalidOperationException("Falied to close the topic");
        }
    }
}