namespace woofli_be_v2._0.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<woofli_be_v2._0.DAL.AuthContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(woofli_be_v2._0.DAL.AuthContext context)
        {
            context.Pets.AddOrUpdate(
                p => p.Name,
                new Pet { Name = "Lunchbox", BirthDate = DateTime.Now, Owner = context.Users.FirstOrDefault(u => u.UserName == "Matt") }
                );

            context.Petsitters.AddOrUpdate(
                p => p.FirstName,
                new Petsitter { FirstName = "Test", LastName = "Person", Email = "test@test.com", Phone = "555-555-5555"}
                );


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
        }
    }
}
