namespace Exebite.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomerAliasesEntities",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Alias = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customer", t => t.Id)
                .ForeignKey("dbo.Restaurant", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AppUserId = c.String(),
                        LocationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Location", t => t.LocationId, cascadeDelete: true)
                .Index(t => t.LocationId);
            
            CreateTable(
                "dbo.Location",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.DateTime(nullable: false),
                        Note = c.String(),
                        MealId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customer", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Meal", t => t.MealId, cascadeDelete: true)
                .Index(t => t.MealId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Meal",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Food",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Type = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(),
                        IsInactive = c.Boolean(nullable: false),
                        RestaurantId = c.Int(nullable: false),
                        RecipeEntity_Id = c.Int(),
                        RestaurantEntity_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Recipe", t => t.RecipeEntity_Id)
                .ForeignKey("dbo.Restaurant", t => t.RestaurantEntity_Id)
                .ForeignKey("dbo.Restaurant", t => t.RestaurantId, cascadeDelete: true)
                .Index(t => t.RestaurantId)
                .Index(t => t.RecipeEntity_Id)
                .Index(t => t.RestaurantEntity_Id);
            
            CreateTable(
                "dbo.Recipe",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MainCourseId = c.Int(nullable: false),
                        FoodEntity_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Food", t => t.MainCourseId, cascadeDelete: true)
                .ForeignKey("dbo.Food", t => t.FoodEntity_Id)
                .Index(t => t.MainCourseId)
                .Index(t => t.FoodEntity_Id);
            
            CreateTable(
                "dbo.Restaurant",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FoodEntityMealEntities",
                c => new
                    {
                        FoodEntity_Id = c.Int(nullable: false),
                        MealEntity_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FoodEntity_Id, t.MealEntity_Id })
                .ForeignKey("dbo.Food", t => t.FoodEntity_Id, cascadeDelete: true)
                .ForeignKey("dbo.Meal", t => t.MealEntity_Id, cascadeDelete: true)
                .Index(t => t.FoodEntity_Id)
                .Index(t => t.MealEntity_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomerAliasesEntities", "Id", "dbo.Restaurant");
            DropForeignKey("dbo.CustomerAliasesEntities", "Id", "dbo.Customer");
            DropForeignKey("dbo.Order", "MealId", "dbo.Meal");
            DropForeignKey("dbo.Food", "RestaurantId", "dbo.Restaurant");
            DropForeignKey("dbo.Food", "RestaurantEntity_Id", "dbo.Restaurant");
            DropForeignKey("dbo.Recipe", "FoodEntity_Id", "dbo.Food");
            DropForeignKey("dbo.Recipe", "MainCourseId", "dbo.Food");
            DropForeignKey("dbo.Food", "RecipeEntity_Id", "dbo.Recipe");
            DropForeignKey("dbo.FoodEntityMealEntities", "MealEntity_Id", "dbo.Meal");
            DropForeignKey("dbo.FoodEntityMealEntities", "FoodEntity_Id", "dbo.Food");
            DropForeignKey("dbo.Order", "CustomerId", "dbo.Customer");
            DropForeignKey("dbo.Customer", "LocationId", "dbo.Location");
            DropIndex("dbo.FoodEntityMealEntities", new[] { "MealEntity_Id" });
            DropIndex("dbo.FoodEntityMealEntities", new[] { "FoodEntity_Id" });
            DropIndex("dbo.Recipe", new[] { "FoodEntity_Id" });
            DropIndex("dbo.Recipe", new[] { "MainCourseId" });
            DropIndex("dbo.Food", new[] { "RestaurantEntity_Id" });
            DropIndex("dbo.Food", new[] { "RecipeEntity_Id" });
            DropIndex("dbo.Food", new[] { "RestaurantId" });
            DropIndex("dbo.Order", new[] { "CustomerId" });
            DropIndex("dbo.Order", new[] { "MealId" });
            DropIndex("dbo.Customer", new[] { "LocationId" });
            DropIndex("dbo.CustomerAliasesEntities", new[] { "Id" });
            DropTable("dbo.FoodEntityMealEntities");
            DropTable("dbo.Restaurant");
            DropTable("dbo.Recipe");
            DropTable("dbo.Food");
            DropTable("dbo.Meal");
            DropTable("dbo.Order");
            DropTable("dbo.Location");
            DropTable("dbo.Customer");
            DropTable("dbo.CustomerAliasesEntities");
        }
    }
}
