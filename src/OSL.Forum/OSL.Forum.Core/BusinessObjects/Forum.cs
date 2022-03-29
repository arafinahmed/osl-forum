﻿using System;
using System.Collections.Generic;

namespace OSL.Forum.Core.BusinessObjects
{
    public class Forum
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public Guid CategoryId { get; set; }
        public string ApplicationUserId { get; set; }
        public virtual Category Category { get; set; }
        public virtual IList<Topic> Topics { get; set; }
    }
}
