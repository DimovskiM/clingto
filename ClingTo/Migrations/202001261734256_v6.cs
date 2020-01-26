namespace ClingTo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v6 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                        ReferenceImage = c.String(nullable: false),
                        Uid = c.Guid(nullable: false),
                        Customer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .Index(t => t.Customer_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Requests", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.Requests", new[] { "Customer_Id" });
            DropTable("dbo.Requests");
        }
    }
}
