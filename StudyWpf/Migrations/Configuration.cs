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

        protected override void Seed(StudyWpf.Models.TestContext testContext)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            base.Seed(testContext);
            // ‚±‚±‚ÅAUpdate-Database‚É‚ÄASeed‚ªŽÀŽ{‚³‚ê‚éBApp_StartŽž‚Í“®‚©‚È‚¢

            var data = Enumerable.Range(1, 1000).Select(s => new TestEntity() { Id = s, Name = "test" + s, SheetId = s ^ 2, ResultStatus = TestEntity.ResultStatusDefinition.NotYet });
            var data2 = Enumerable.Range(1, 1000).Select(s => new Test2Entity() { Id = s, Name = "test" + s, TestModelId = s, ResultStatus = Test2Entity.Test2Status.NotYet });

            testContext.Items.AddOrUpdate(
                p => p.Id,
                data.ToArray()
            );
            testContext.Item2s.AddOrUpdate(
                p => p.Id,
                data2.ToArray()
            );
            testContext.SaveChanges();
        }
    }
}
