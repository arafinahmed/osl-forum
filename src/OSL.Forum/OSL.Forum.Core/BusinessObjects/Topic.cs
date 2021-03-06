using System;
using System.Collections.Generic;

namespace OSL.Forum.Core.BusinessObjects
{
    public class Topic
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public Guid ForumId { get; set; }
        public virtual Forum Forum { get; set; }
        public string ApplicationUserId { get; set; }
        public string ApprovalType { get; set; }
        public virtual IList<Post> Posts { get; set; }
        public bool Owner { get; set; }
        public string Status { get; set; }
    }
}
