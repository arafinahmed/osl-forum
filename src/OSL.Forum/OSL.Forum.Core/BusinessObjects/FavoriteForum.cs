using System;

namespace OSL.Forum.Core.BusinessObjects
{
    public class FavoriteForum
    {
 
        public Guid Id { get; set; }
        public string ApplicationUserId { get; set; }
        public Guid ForumId { get; set; }
        public virtual Forum Forum { get; set; }
    }
}
