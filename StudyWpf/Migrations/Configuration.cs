namespace StudyWpf.Migrations
{
    using StudyWpf.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.SQLite.EF6.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<StudyWpf.Models.TestContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            SetSqlGenerator("System.Data.SQLite.EF6", new SQLiteMigrationSqlGenerator());
        }

        protected override void Seed(StudyWpf.Models.TestContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.

            context.Items.AddOrUpdate(
              p => p.Id,
              new TestModel() {Id = 1, Name = "Andrew Peters" , SheetId = 2, ResultStatus = TestModel.ResultStatusDefinition.NotYet},
              new TestModel() {Id = 2, Name = "Brice Lambson", SheetId = 2, ResultStatus = TestModel.ResultStatusDefinition.NotYet },
              new TestModel() {Id = 3, Name = "Rowan Miller", SheetId = 2, ResultStatus = TestModel.ResultStatusDefinition.NotYet }
            );
            context.SaveChanges();
        }
    }
}
