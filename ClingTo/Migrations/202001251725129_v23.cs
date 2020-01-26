namespace ClingTo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v23 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "Address", c => c.String(nullable: false));
            AddColumn("dbo.Customers", "City", c => c.String());
            AddColumn("dbo.Customers", "ZipCode", c => c.Int(nullable: false));
            AddColumn("dbo.Customers", "Country", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "Country");
            DropColumn("dbo.Customers", "ZipCode");
            DropColumn("dbo.Customers", "City");
            DropColumn("dbo.Customers", "Address");
        }
    }
}
