namespace Exebite.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixAliasEnt : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CustomerAliasesEntities", "Id", "dbo.Customer");
            DropForeignKey("dbo.CustomerAliasesEntities", "Id", "dbo.Restaurant");
            DropIndex("dbo.CustomerAliasesEntities", new[] { "Id" });
            DropPrimaryKey("dbo.CustomerAliasesEntities");
            AddColumn("dbo.CustomerAliasesEntities", "Customer_Id", c => c.Int(nullable: false));
            AddColumn("dbo.CustomerAliasesEntities", "Restaurant_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.CustomerAliasesEntities", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.CustomerAliasesEntities", "Id");
            CreateIndex("dbo.CustomerAliasesEntities", "Customer_Id");
            CreateIndex("dbo.CustomerAliasesEntities", "Restaurant_Id");
            AddForeignKey("dbo.CustomerAliasesEntities", "Customer_Id", "dbo.Customer", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CustomerAliasesEntities", "Restaurant_Id", "dbo.Restaurant", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomerAliasesEntities", "Restaurant_Id", "dbo.Restaurant");
            DropForeignKey("dbo.CustomerAliasesEntities", "Customer_Id", "dbo.Customer");
            DropIndex("dbo.CustomerAliasesEntities", new[] { "Restaurant_Id" });
            DropIndex("dbo.CustomerAliasesEntities", new[] { "Customer_Id" });
            DropPrimaryKey("dbo.CustomerAliasesEntities");
            AlterColumn("dbo.CustomerAliasesEntities", "Id", c => c.Int(nullable: false));
            DropColumn("dbo.CustomerAliasesEntities", "Restaurant_Id");
            DropColumn("dbo.CustomerAliasesEntities", "Customer_Id");
            AddPrimaryKey("dbo.CustomerAliasesEntities", "Id");
            CreateIndex("dbo.CustomerAliasesEntities", "Id");
            AddForeignKey("dbo.CustomerAliasesEntities", "Id", "dbo.Restaurant", "Id");
            AddForeignKey("dbo.CustomerAliasesEntities", "Id", "dbo.Customer", "Id");
        }
    }
}
