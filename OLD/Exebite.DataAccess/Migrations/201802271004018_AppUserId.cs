namespace Exebite.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AppUserId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customer", "AppUserId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customer", "AppUserId");
        }
    }
}
