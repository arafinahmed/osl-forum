using OSL.Forum.DataAccessLayer.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OSL.Forum.Core.Entities
{
    [Table("FavoriteForums")]
    public class FavoriteForum : IEntity<Guid>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string ApplicationUserId { get; set; }
        [ForeignKey("Forum")]
        public Guid ForumId { get; set; }
        public virtual Forum Forum { get; set; }
    }
}
