namespace OSL.Forum.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StatusInTopicTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Topics", "Status", c => c.String(maxLength: 8));
            AlterColumn("dbo.Topics", "ApprovalType", c => c.String(maxLength: 8));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Topics", "ApprovalType", c => c.String());
            DropColumn("dbo.Topics", "Status");
        }
    }
}
