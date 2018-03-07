namespace Exebite.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFoodIsInactive : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Food", "IsInactive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Food", "IsInactive");
        }
    }
}
