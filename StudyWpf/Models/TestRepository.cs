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
            return testContext.Items.Take(10);
        }
        public IEnumerable<Test2Entity> Test2s()
        {
            return testContext.Item2s.Take(10);
        }
    }
}
