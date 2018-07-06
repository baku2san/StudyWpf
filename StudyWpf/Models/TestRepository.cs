using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudyWpf.Models
{
    public class TestRepository
    {
        private TestContext testContext = new TestContext();

        public IEnumerable<TestEntity> Tests()
        {
            return testContext.Items;
        }
        public IEnumerable<Test2Entity> Test2s(TestEntity test)
        {
            return testContext.Item2s.Where(w=> w.TestModelId == test.Id);
        }

        internal IEnumerable<Test3Entity> Test3s(Test2Entity selectedTest)
        {
            return testContext.Item3s.Where(w => w.Test2Id == selectedTest.Id);
        }
    }
}
