namespace ClingTo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v7 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Orders", "CartId", "dbo.Carts");
            DropIndex("dbo.Orders", new[] { "CustomerId" });
            DropIndex("dbo.Orders", new[] { "CartId" });
            RenameColumn(table: "dbo.Orders", name: "CustomerId", newName: "Customer_Id");
            RenameColumn(table: "dbo.Orders", name: "CartId", newName: "Cart_Id");
            AddColumn("dbo.Customers", "Email", c => c.String());
            AlterColumn("dbo.Customers", "FullName", c => c.String());
            AlterColumn("dbo.Customers", "Address", c => c.String());
            AlterColumn("dbo.Orders", "Customer_Id", c => c.Int());
            AlterColumn("dbo.Orders", "Cart_Id", c => c.Int());
            CreateIndex("dbo.Orders", "Cart_Id");
            CreateIndex("dbo.Orders", "Customer_Id");
            AddForeignKey("dbo.Orders", "Customer_Id", "dbo.Customers", "Id");
            AddForeignKey("dbo.Orders", "Cart_Id", "dbo.Carts", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "Cart_Id", "dbo.Carts");
            DropForeignKey("dbo.Orders", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.Orders", new[] { "Customer_Id" });
            DropIndex("dbo.Orders", new[] { "Cart_Id" });
            AlterColumn("dbo.Orders", "Cart_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Orders", "Customer_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Customers", "Address", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "FullName", c => c.String(nullable: false, maxLength: 300));
            DropColumn("dbo.Customers", "Email");
            RenameColumn(table: "dbo.Orders", name: "Cart_Id", newName: "CartId");
            RenameColumn(table: "dbo.Orders", name: "Customer_Id", newName: "CustomerId");
            CreateIndex("dbo.Orders", "CartId");
            CreateIndex("dbo.Orders", "CustomerId");
            AddForeignKey("dbo.Orders", "CartId", "dbo.Carts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Orders", "CustomerId", "dbo.Customers", "Id", cascadeDelete: true);
        }
    }
}
