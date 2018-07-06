namespace StudyWpf.Migrations
{
    using StudyWpf.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.SQLite.EF6.Migrations;
    using System.Linq;
    using static StudyWpf.Models.Constants;

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
            // ここで、Update-Databaseにて、Seedが実施される。App_Start時は動かない

            var random = new Random();
            var data = Enumerable.Range(1, 100).Select(s => new TestEntity() { Id = s, Name = "test" + s, SheetId = s ^ 2, ResultStatus = (ResultStatus)random.Next(3)});
            var data2 = Enumerable.Range(1, 1000).Select(s => new Test2Entity() { Id = s, Name = Enumerable.Range(1,5).Select(ss=>((char)random.Next(65, 90)).ToString()).Aggregate((f,se)=>f+se), TestModelId = random.Next(100)+1, ResultStatus = (ResultStatus)random.Next(3), IsOk = true });
            var data3 = Enumerable.Range(1, 1000).Select(s => new Test3Entity() { Id = s, Name = Enumerable.Range(1,5).Select(ss=>((char)random.Next(65, 90)).ToString()).Aggregate((f,se)=>f+se), Test2Id = random.Next(100)+1, ResultStatus = (ResultStatus)random.Next(3), IsOk = true });

            testContext.Items.AddOrUpdate(
                p => p.Id,
                data.ToArray()
            );
            testContext.Item2s.AddOrUpdate(
                p => p.Id,
                data2.ToArray()
            );
            testContext.Item3s.AddOrUpdate(
                p => p.Id,
                data3.ToArray()
            );
            testContext.SaveChanges();
        }
    }
}
