using System;
using System.Collections.Generic;

namespace OSL.Forum.Core.BusinessObjects
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public virtual IList<Forum> Forums { get; set; }
    }
}
