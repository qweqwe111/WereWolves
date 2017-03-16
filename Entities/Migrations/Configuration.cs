namespace Entities.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Wwa.Entities;

    internal sealed class Configuration : DbMigrationsConfiguration<Wwa.Entities.MysqlDbContext>
    {
        public Configuration()
        {
            SetSqlGenerator("MySql.Data.MySqlClient", new MySql.Data.Entity.MySqlMigrationSqlGenerator());
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<MysqlDbContext, Configuration>());
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Wwa.Entities.MysqlDbContext context)
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
        }
    }
}
