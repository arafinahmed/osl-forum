using OSL.Forum.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OSL.Forum.Core.Entities
{
    [Table("Topics")]
    public class Topic : IEntity<Guid>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(64)]
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public Guid ForumId { get; set; }
        public virtual Forum Forum { get; set; }
        public string ApplicationUserId { get; set; }
        [MaxLength(8)]
        public string ApprovalType { get; set; }
        public virtual IList<Post> Posts { get; set; }
        [MaxLength(8)]
        public string Status { get; set; }
    }
}
