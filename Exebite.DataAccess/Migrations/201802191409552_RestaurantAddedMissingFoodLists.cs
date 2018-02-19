namespace Exebite.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RestaurantAddedMissingFoodLists : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Food", "RestaurantId", "dbo.Restaurant");
            AddColumn("dbo.Food", "RestaurantEntity_Id", c => c.Int());
            AddColumn("dbo.Food", "RestaurantEntity_Id1", c => c.Int());
            AddColumn("dbo.Food", "RestaurantEntity_Id2", c => c.Int());
            CreateIndex("dbo.Food", "RestaurantEntity_Id");
            CreateIndex("dbo.Food", "RestaurantEntity_Id1");
            CreateIndex("dbo.Food", "RestaurantEntity_Id2");
            AddForeignKey("dbo.Food", "RestaurantEntity_Id", "dbo.Restaurant", "Id");
            AddForeignKey("dbo.Food", "RestaurantEntity_Id1", "dbo.Restaurant", "Id");
            AddForeignKey("dbo.Food", "RestaurantEntity_Id2", "dbo.Restaurant", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Food", "RestaurantEntity_Id2", "dbo.Restaurant");
            DropForeignKey("dbo.Food", "RestaurantEntity_Id1", "dbo.Restaurant");
            DropForeignKey("dbo.Food", "RestaurantEntity_Id", "dbo.Restaurant");
            DropIndex("dbo.Food", new[] { "RestaurantEntity_Id2" });
            DropIndex("dbo.Food", new[] { "RestaurantEntity_Id1" });
            DropIndex("dbo.Food", new[] { "RestaurantEntity_Id" });
            DropColumn("dbo.Food", "RestaurantEntity_Id2");
            DropColumn("dbo.Food", "RestaurantEntity_Id1");
            DropColumn("dbo.Food", "RestaurantEntity_Id");
            AddForeignKey("dbo.Food", "RestaurantId", "dbo.Restaurant", "Id", cascadeDelete: true);
        }
    }
}
