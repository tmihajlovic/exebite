namespace Exebite.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixAliasEnt1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CustomerAliasesEntities", "Customer_Id", "dbo.Customer");
            DropForeignKey("dbo.CustomerAliasesEntities", "Restaurant_Id", "dbo.Restaurant");
            DropIndex("dbo.CustomerAliasesEntities", new[] { "Customer_Id" });
            DropIndex("dbo.CustomerAliasesEntities", new[] { "Restaurant_Id" });
            AlterColumn("dbo.CustomerAliasesEntities", "Customer_Id", c => c.Int());
            AlterColumn("dbo.CustomerAliasesEntities", "Restaurant_Id", c => c.Int());
            CreateIndex("dbo.CustomerAliasesEntities", "Customer_Id");
            CreateIndex("dbo.CustomerAliasesEntities", "Restaurant_Id");
            AddForeignKey("dbo.CustomerAliasesEntities", "Customer_Id", "dbo.Customer", "Id");
            AddForeignKey("dbo.CustomerAliasesEntities", "Restaurant_Id", "dbo.Restaurant", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomerAliasesEntities", "Restaurant_Id", "dbo.Restaurant");
            DropForeignKey("dbo.CustomerAliasesEntities", "Customer_Id", "dbo.Customer");
            DropIndex("dbo.CustomerAliasesEntities", new[] { "Restaurant_Id" });
            DropIndex("dbo.CustomerAliasesEntities", new[] { "Customer_Id" });
            AlterColumn("dbo.CustomerAliasesEntities", "Restaurant_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.CustomerAliasesEntities", "Customer_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.CustomerAliasesEntities", "Restaurant_Id");
            CreateIndex("dbo.CustomerAliasesEntities", "Customer_Id");
            AddForeignKey("dbo.CustomerAliasesEntities", "Restaurant_Id", "dbo.Restaurant", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CustomerAliasesEntities", "Customer_Id", "dbo.Customer", "Id", cascadeDelete: true);
        }
    }
}
