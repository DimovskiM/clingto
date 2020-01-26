namespace ClingTo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Uid = c.Guid(nullable: false),
                        Customer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .Index(t => t.Customer_Id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        FullName = c.String(nullable: false, maxLength: 300),
                        Uid = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        CartId = c.Int(nullable: false),
                        Uid = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Carts", t => t.CartId, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.CartId);
            
            AddColumn("dbo.Products", "Description", c => c.String(nullable: false));
            AddColumn("dbo.Products", "Cart_Id", c => c.Int());
            AddColumn("dbo.AspNetUsers", "Customer_Id", c => c.Int());
            CreateIndex("dbo.Products", "Cart_Id");
            CreateIndex("dbo.AspNetUsers", "Customer_Id");
            AddForeignKey("dbo.Products", "Cart_Id", "dbo.Carts", "Id");
            AddForeignKey("dbo.AspNetUsers", "Customer_Id", "dbo.Customers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.Orders", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Orders", "CartId", "dbo.Carts");
            DropForeignKey("dbo.Carts", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.Products", "Cart_Id", "dbo.Carts");
            DropIndex("dbo.AspNetUsers", new[] { "Customer_Id" });
            DropIndex("dbo.Orders", new[] { "CartId" });
            DropIndex("dbo.Orders", new[] { "CustomerId" });
            DropIndex("dbo.Products", new[] { "Cart_Id" });
            DropIndex("dbo.Carts", new[] { "Customer_Id" });
            DropColumn("dbo.AspNetUsers", "Customer_Id");
            DropColumn("dbo.Products", "Cart_Id");
            DropColumn("dbo.Products", "Description");
            DropTable("dbo.Orders");
            DropTable("dbo.Customers");
            DropTable("dbo.Carts");
        }
    }
}
