namespace ClingTo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v9 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Products", "Quantity");
            DropColumn("dbo.Products", "Size");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Size", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "Quantity", c => c.Int(nullable: false));
        }
    }
}
