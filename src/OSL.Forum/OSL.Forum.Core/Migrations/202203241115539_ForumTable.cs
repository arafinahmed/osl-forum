namespace OSL.Forum.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ForumTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Forums",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 64),
                        CreationDate = c.DateTime(nullable: false),
                        ModificationDate = c.DateTime(nullable: false),
                        CategoryId = c.Guid(nullable: false),
                        ApplicationUserId = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Forums", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Forums", new[] { "CategoryId" });
            DropTable("dbo.Forums");
        }
    }
}
