namespace ClingTo.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ClingTo.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "ClingTo.Models.ApplicationDbContext";
        }

        protected override void Seed(ClingTo.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            //context.Products.AddOrUpdate(new Models.Product
            //{
            //   Name = "Socks",
            //   Description = "A Pair of Socks",
            //   ImageUrl = "https://www.minirodini.com/media/catalog/product/cache/a77b7849fc1a89605515cac650fbd86d/1/9/1976012323-1.jpg",
            //   Price = 100m,
            //   Size = Models.ProductSize.L,
            //   Uid = Guid.NewGuid()
            //});
        }
    }
}
