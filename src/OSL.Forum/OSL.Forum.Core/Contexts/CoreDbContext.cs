using EO = OSL.Forum.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSL.Forum.Core.Contexts
{
    public class CoreDbContext : DbContext, ICoreDbContext
    {
        public CoreDbContext() : base("DefaultConnection")
        {

        }

        public CoreDbContext(string connectionString) : base(connectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EO.Category>()
                .HasKey(c => c.Id)
                .HasMany(c => c.Forums);

            modelBuilder.Entity<EO.Forum>()
                .HasKey(f => f.Id)
                .HasRequired(f => f.Category)
                .WithMany(c => c.Forums)
                .HasForeignKey(f => f.CategoryId);

            base.OnModelCreating(modelBuilder);
        }

        DbSet<EO.Category> Categories { get; set; }
        DbSet<EO.Forum> Forums{ get; set; }
    }
}
