using SQLite.CodeFirst;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyWpf.Models
{
    public class TestContext : DbContext
    {
        public DbSet<TestModel> Items { get; set; }

        public TestContext() : base("name=SqliteInFile")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // ★現時点での結論
            // 不明：__migration_history が何処で実施されるか？
            // 可能：Update-Database なら、__migration_history はAlwaysもIfNotExists も問題なし
            // 不可：Application 実行時だと、__migration_history が無いDBが出来てしまうので問題。SeedをやろうとするとCreate必要なんで、現状方法不明のまま・・
            base.OnModelCreating(modelBuilder);
            var sqliteConnectionInitializer = new TestContextCreater(modelBuilder);
            //var sqliteConnectionInitializer = new SqliteDropCreateDatabaseWhenModelChanges<TestContext>(modelBuilder);
            Database.SetInitializer(sqliteConnectionInitializer);
            //Seed(); // 直接ここで書いてしまうと、Update-Database時にはContextとして有効でないからOut
        }
        //public void Seed()
        //{
        //    this.Items.AddOrUpdate(
        //        p => p.Id,
        //        new TestModel() { Id = 1, Name = "Andrew Peters", SheetId = 2, ResultStatus = TestModel.ResultStatusDefinition.NotYet },
        //        new TestModel() { Id = 2, Name = "Brice Lambson", SheetId = 2, ResultStatus = TestModel.ResultStatusDefinition.NotYet },
        //        new TestModel() { Id = 3, Name = "Rowan Miller", SheetId = 2, ResultStatus = TestModel.ResultStatusDefinition.NotYet }
        //    );
        //    this.SaveChanges();
        //}
    }
    // Update-だとMigrationHistoryあるけど
    // プログラムだとTableしかない・・なんで？

    // 2nd : specific creator
    // Program ○
    // Update-Database ×: Configuration でやらないと無理っぽい
    public class TestContextCreater: SqliteCreateDatabaseIfNotExists<TestContext>
    {
        public TestContextCreater(DbModelBuilder modelBuilder) : base(modelBuilder)
        {

        }
        protected override void Seed(TestContext testContext)
        {
            base.Seed(testContext);
            SeedAbs(testContext);
        }
        static public void SeedAbs(TestContext testContext)
        {
            testContext.Items.AddOrUpdate(
                p => p.Id,
                new TestModel() { Id = 1, Name = "Andrew Peters", SheetId = 3, ResultStatus = TestModel.ResultStatusDefinition.NotYet },
                new TestModel() { Id = 2, Name = "Brice Lambson", SheetId = 2, ResultStatus = TestModel.ResultStatusDefinition.NotYet },
                new TestModel() { Id = 3, Name = "Rowan Miller", SheetId = 2, ResultStatus = TestModel.ResultStatusDefinition.NotYet }
            );
            testContext.SaveChanges();

        }
    }

    // 1st : 
    public class SqLites<TContext> : SqliteDropCreateDatabaseAlways<TContext> where TContext : DbContext
    {
        public SqLites(DbModelBuilder modelBuilder): base(modelBuilder)
        {

        }
        protected override void Seed(TContext context)
        {
            base.Seed(context);

            var testContext = context as TestContext;
            if (testContext != null)
            {
                testContext.Items.AddOrUpdate(
                  p => p.Id,
                  new TestModel() { Id = 1, Name = "Andrew Peters", SheetId = 2, ResultStatus = TestModel.ResultStatusDefinition.NotYet },
                  new TestModel() { Id = 2, Name = "Brice Lambson", SheetId = 2, ResultStatus = TestModel.ResultStatusDefinition.NotYet },
                  new TestModel() { Id = 3, Name = "Rowan Miller", SheetId = 2, ResultStatus = TestModel.ResultStatusDefinition.NotYet }
                );
                testContext.SaveChanges();
            }
        }
    }
}
